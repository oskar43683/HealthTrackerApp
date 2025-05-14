# Health Tracker App

A cross-platform application for tracking workouts and health metrics.

## Available Versions

### Windows Forms Version (.NET)
A Windows Forms application for tracking workouts and health metrics.

#### How to Run (Windows)

1. Make sure you have .NET 6.0 SDK installed on your Windows machine
2. Open a terminal in the project directory
3. Run these commands:
   ```
   dotnet restore
   dotnet build
   dotnet run
   ```

The app will launch as a Windows Forms application in a new window.

### Qt Version (Cross-Platform)
A Qt/C++ version that runs natively on Linux, Windows, and macOS.

#### How to Run (Linux)

1. Install Qt6 development tools:
   ```bash
   sudo apt-get update
   sudo apt-get install -y qt6-base-dev qt6-base-dev-tools qtcreator
   ```

2. Navigate to the Qt project directory:
   ```bash
   cd HealthTrackerQt
   ```

3. Build and run the application:
   ```bash
   qmake6
   make
   ./HealthTrackerQt
   ```

#### How to Run (Windows/macOS)

For Windows and macOS, you can:
1. Install Qt Creator IDE from [qt.io](https://www.qt.io/download)
2. Open the `HealthTrackerQt.pro` file in Qt Creator
3. Build and run the project from the IDE

## Features

- Add workouts with date, exercise type, duration, and calories burned
- Optional notes for each workout
- Save and load workout data from file
- List view of all recorded workouts
- Cross-platform compatibility (Qt version)

## Exercise Types Supported

- Running
- Cycling
- Swimming
- Weight Training
- Yoga
- Other

## Data Storage

Workout data is stored in a simple text file (`workouts.txt`) in the application directory, making it easy to backup or transfer your data.