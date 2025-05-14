#include "mainwindow.h"
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QLabel>
#include <QPushButton>
#include <QMessageBox>
#include <QFile>
#include <QTextStream>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
{
    setupUi();
    loadWorkouts();
}

MainWindow::~MainWindow()
{
}

void MainWindow::setupUi()
{
    setWindowTitle("Health & Training Tracker");
    resize(800, 600);

    QWidget* centralWidget = new QWidget(this);
    setCentralWidget(centralWidget);
    QVBoxLayout* mainLayout = new QVBoxLayout(centralWidget);

    // Date input
    QHBoxLayout* dateLayout = new QHBoxLayout();
    QLabel* dateLabel = new QLabel("Date:", this);
    m_dateEdit = new QDateTimeEdit(QDateTime::currentDateTime(), this);
    m_dateEdit->setCalendarPopup(true);
    dateLayout->addWidget(dateLabel);
    dateLayout->addWidget(m_dateEdit);
    mainLayout->addLayout(dateLayout);

    // Exercise type input
    QHBoxLayout* exerciseLayout = new QHBoxLayout();
    QLabel* exerciseLabel = new QLabel("Exercise Type:", this);
    m_exerciseTypeCombo = new QComboBox(this);
    m_exerciseTypeCombo->addItems({"Running", "Cycling", "Swimming", "Weight Training", "Yoga", "Other"});
    exerciseLayout->addWidget(exerciseLabel);
    exerciseLayout->addWidget(m_exerciseTypeCombo);
    mainLayout->addLayout(exerciseLayout);

    // Duration input
    QHBoxLayout* durationLayout = new QHBoxLayout();
    QLabel* durationLabel = new QLabel("Duration:", this);
    m_hoursSpinBox = new QSpinBox(this);
    m_hoursSpinBox->setRange(0, 24);
    QLabel* hoursLabel = new QLabel("hours", this);
    m_minutesSpinBox = new QSpinBox(this);
    m_minutesSpinBox->setRange(0, 59);
    QLabel* minutesLabel = new QLabel("minutes", this);
    durationLayout->addWidget(durationLabel);
    durationLayout->addWidget(m_hoursSpinBox);
    durationLayout->addWidget(hoursLabel);
    durationLayout->addWidget(m_minutesSpinBox);
    durationLayout->addWidget(minutesLabel);
    mainLayout->addLayout(durationLayout);

    // Calories input
    QHBoxLayout* caloriesLayout = new QHBoxLayout();
    QLabel* caloriesLabel = new QLabel("Calories burned:", this);
    m_caloriesEdit = new QLineEdit(this);
    caloriesLayout->addWidget(caloriesLabel);
    caloriesLayout->addWidget(m_caloriesEdit);
    mainLayout->addLayout(caloriesLayout);

    // Notes input
    QLabel* notesLabel = new QLabel("Notes:", this);
    m_notesEdit = new QTextEdit(this);
    mainLayout->addWidget(notesLabel);
    mainLayout->addWidget(m_notesEdit);

    // Add button
    QPushButton* addButton = new QPushButton("Add Workout", this);
    connect(addButton, &QPushButton::clicked, this, &MainWindow::addWorkout);
    mainLayout->addWidget(addButton);

    // Workout list
    m_workoutList = new QListWidget(this);
    mainLayout->addWidget(m_workoutList);
}

void MainWindow::addWorkout()
{
    bool ok;
    int calories = m_caloriesEdit->text().toInt(&ok);
    if (!ok || m_exerciseTypeCombo->currentText().isEmpty())
    {
        QMessageBox::warning(this, "Error", "Please fill in all required fields with valid values.");
        return;
    }

    int totalMinutes = m_hoursSpinBox->value() * 60 + m_minutesSpinBox->value();
    if (totalMinutes == 0)
    {
        QMessageBox::warning(this, "Error", "Duration must be greater than 0 minutes.");
        return;
    }

    Workout workout(
        m_dateEdit->dateTime(),
        m_exerciseTypeCombo->currentText(),
        totalMinutes,
        calories,
        m_notesEdit->toPlainText()
    );

    m_workouts.append(workout);
    saveWorkouts();
    refreshWorkoutList();

    // Clear inputs
    m_exerciseTypeCombo->setCurrentIndex(-1);
    m_hoursSpinBox->setValue(0);
    m_minutesSpinBox->setValue(0);
    m_caloriesEdit->clear();
    m_notesEdit->clear();
}

void MainWindow::loadWorkouts()
{
    QFile file(m_dataFile);
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
        return;

    QTextStream in(&file);
    while (!in.atEnd())
    {
        QString line = in.readLine();
        QStringList parts = line.split('|');
        if (parts.size() >= 4)
        {
            Workout workout(
                QDateTime::fromString(parts[0], Qt::ISODate),
                parts[1],
                parts[2].toInt(),
                parts[3].toInt(),
                parts.size() > 4 ? parts[4] : ""
            );
            m_workouts.append(workout);
        }
    }

    refreshWorkoutList();
}

void MainWindow::saveWorkouts()
{
    QFile file(m_dataFile);
    if (!file.open(QIODevice::WriteOnly | QIODevice::Text))
    {
        QMessageBox::warning(this, "Error", "Could not save workouts to file.");
        return;
    }

    QTextStream out(&file);
    for (const Workout& workout : m_workouts)
    {
        out << workout.getDate().toString(Qt::ISODate) << "|"
            << workout.getExerciseType() << "|"
            << workout.getDurationMinutes() << "|"
            << workout.getCaloriesBurned() << "|"
            << workout.getNotes() << "\n";
    }
}

void MainWindow::refreshWorkoutList()
{
    m_workoutList->clear();
    for (const Workout& workout : m_workouts)
    {
        m_workoutList->addItem(workout.toString());
    }
} 