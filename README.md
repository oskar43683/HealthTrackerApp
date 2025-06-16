# Health Tracker Application

A modern, user-friendly desktop application for tracking personal fitness activities and workouts. Built with C# and Windows Forms, this application provides a secure and intuitive interface for managing your fitness journey.

## Features

### User Authentication
- Secure user registration and login system
- Password hashing using SHA256 for enhanced security
- User-specific data storage and access control

### Workout Tracking
- Record various types of exercises:
  - Running
  - Cycling
  - Swimming
  - Weight Training
  - Yoga
  - Other activities
- Track workout details:
  - Date and time
  - Duration (hours and minutes)
  - Calories burned
  - Personal notes
- View workout history in a clean, organized list

### User Interface
- Modern dark theme design
- Responsive and intuitive layout
- Real-time data updates
- User-friendly input forms
- Interactive buttons with hover effects

## Technical Details

### System Requirements
- Windows operating system
- .NET 6.0 or later
- Minimum screen resolution: 1024x768

### Data Storage
The application uses a secure file-based storage system:
- User data is stored in individual directories under the `Users` folder
- Each user's directory contains:
  - `user.txt`: Encrypted user credentials
  - `workouts.txt`: Workout history and data

### Security Features
- Password hashing using SHA256
- User-specific data isolation
- Secure file storage structure
- Protected user directories

## Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/HealthTrackerApp.git
cd HealthTrackerApp
```

2. Open the solution in Visual Studio 2022 or later

3. Build the solution:
```bash
dotnet build
```

4. Run the application:
```bash
# Option 1: Run from project root
dotnet run --project src/HealthTrackerApp.csproj

# Option 2: Navigate to src folder and run
cd src
dotnet run
```

## Usage

### First-time Setup
1. Launch the application
2. Click "Register New Account"
3. Enter your desired username and password
4. Click "Register"

### Daily Use
1. Launch the application
2. Log in with your credentials
3. To add a workout:
   - Select the date
   - Choose the exercise type
   - Enter the duration
   - Input calories burned
   - Add any notes
   - Click "Add Workout"
4. View your workout history in the list panel

## Testing

### Running Tests
The project includes a comprehensive test suite using xUnit. To run the tests:

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "FullyQualifiedName~UserTests"

# Run specific test method
dotnet test --filter "FullyQualifiedName~CreateUser_ValidCredentials_ShouldCreateUserDirectory"
```

### Test Coverage
The test suite covers:
- **User Authentication**: Password hashing, user creation, login verification
- **Data Persistence**: File storage, user data loading/saving
- **Workout Management**: Object creation, data formatting, edge cases
- **Integration**: End-to-end user workflows

### Test Structure
```
HealthTrackerApp.Tests/
├── UserTests.cs           # User authentication and data management tests
├── WorkoutTests.cs        # Workout object and formatting tests
└── HealthTrackerApp.Tests.csproj
```

### Writing New Tests
When adding new features, follow these testing guidelines:
1. Create tests for all public methods
2. Test both success and failure scenarios
3. Use descriptive test names that explain the expected behavior
4. Clean up test data in the `Dispose` method
5. Use culture-invariant formats for date/time tests

## Development

### Project Structure
```
HealthTrackerApp/
├── src/                   # Main application source
│   ├── Program.cs         # Application entry point
│   ├── MainForm.cs        # Main application window
│   ├── LoginForm.cs       # User authentication form
│   ├── User.cs            # User management class
│   ├── Workout.cs         # Workout data model
│   └── HealthTrackerApp.csproj
├── HealthTrackerApp.Tests/ # Test project
│   ├── UserTests.cs       # User authentication tests
│   ├── WorkoutTests.cs    # Workout management tests
│   └── HealthTrackerApp.Tests.csproj
├── HealthTrackerApp.sln   # Solution file
└── README.md             # This documentation
```

### Key Components
- `Program.cs`: Handles application startup and form initialization
- `MainForm.cs`: Implements the main workout tracking interface
- `LoginForm.cs`: Manages user authentication
- `User.cs`: Handles user data and security
- `Workout.cs`: Defines the workout data structure

### Building and Running
```bash
# Build the entire solution
dotnet build

# Run the main application
dotnet run --project src/HealthTrackerApp.csproj

# Run tests
dotnet test

# Clean build artifacts
dotnet clean
```

## Troubleshooting

### Common Issues
1. **Tests fail with date format errors**: This is usually due to system locale differences. Tests use culture-invariant date formats.
2. **User data not saving**: Check that the application has write permissions to the directory.
3. **Login issues**: Ensure the user was properly registered and the password is correct.

## Acknowledgments

- Built with C# and Windows Forms
- Uses .NET 6.0 framework
- Implements modern UI/UX practices
- Testing framework: xUnit
