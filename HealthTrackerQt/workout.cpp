#include "workout.h"

Workout::Workout(const QDateTime& date, const QString& exerciseType, int durationMinutes, int caloriesBurned, const QString& notes)
    : m_date(date)
    , m_exerciseType(exerciseType)
    , m_durationMinutes(durationMinutes)
    , m_caloriesBurned(caloriesBurned)
    , m_notes(notes)
{
}

QString Workout::toString() const
{
    return QString("%1 - %2 (%3 min, %4 cal)")
        .arg(m_date.toString("MM/dd/yyyy"))
        .arg(m_exerciseType)
        .arg(m_durationMinutes)
        .arg(m_caloriesBurned);
} 