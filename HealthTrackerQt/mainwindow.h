#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QVector>
#include <QDateTime>
#include <QDateTimeEdit>
#include <QComboBox>
#include <QSpinBox>
#include <QLineEdit>
#include <QTextEdit>
#include <QListWidget>
#include "workout.h"

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void addWorkout();

private:
    void setupUi();
    void loadWorkouts();
    void saveWorkouts();
    void refreshWorkoutList();

    QVector<Workout> m_workouts;
    QString m_dataFile = "workouts.txt";

    QDateTimeEdit* m_dateEdit;
    QComboBox* m_exerciseTypeCombo;
    QSpinBox* m_hoursSpinBox;
    QSpinBox* m_minutesSpinBox;
    QLineEdit* m_caloriesEdit;
    QTextEdit* m_notesEdit;
    QListWidget* m_workoutList;
};

#endif // MAINWINDOW_H 