#ifndef WORKOUT_H
#define WORKOUT_H

#include <QDateTime>
#include <QString>

class Workout {
public:
    Workout(const QDateTime& date, const QString& exerciseType, int durationMinutes, int caloriesBurned, const QString& notes = "");

    QDateTime getDate() const { return m_date; }
    QString getExerciseType() const { return m_exerciseType; }
    int getDurationMinutes() const { return m_durationMinutes; }
    int getCaloriesBurned() const { return m_caloriesBurned; }
    QString getNotes() const { return m_notes; }

    QString toString() const;

private:
    QDateTime m_date;
    QString m_exerciseType;
    int m_durationMinutes;
    int m_caloriesBurned;
    QString m_notes;
};

#endif // WORKOUT_H 