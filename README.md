# Geography Quiz Application (Avalonia UI)

## 1. Project Overview

The Geography Quiz Application is a desktop program built with **C# and Avalonia UI**, designed to test and enhance users' knowledge of world geography, including countries, capitals, and flags. It features multiple quiz modes, configurable quiz sessions, and high score tracking, all within a modern, cross-platform user interface.

This project was refactored from an original Windows Forms concept to Avalonia UI, targeting .NET 8.0. The specifications from the original `AGENTS.md` regarding core logic and features have been adapted to this new framework.

## 2. Core Technologies

*   **Programming Language:** C# (using .NET 8.0 and C# 12)
*   **UI Framework:** Avalonia UI (Version 11.0.10) with Fluent Theme
*   **Architecture:** Model-View-ViewModel (MVVM)
    *   ViewModels inherit from a custom `ViewModelBase`.
    *   ReactiveUI package included, though current ViewModels use basic `ICommand` and `INotifyPropertyChanged`.
*   **Database (Conceptual):** The Data Access Layer (DAL) is designed for MySQL but currently uses simulated in-memory data for portability and ease of testing without a database setup.
*   **IDE:** Microsoft Visual Studio (2022 recommended) or JetBrains Rider.

## 3. Features

*   **Multiple Quiz Modes:** (Country from Flag, Capital from Country, etc.)
*   **Player Management:** Enter player name to track scores.
*   **Continent Filtering:** Filter quizzes by specific continents or play "Worldwide".
*   **Configurable Quiz Length:** Choose the number of questions.
*   **High Score Tracking:** View local high scores, filter by "All Players" or "My Scores".
*   **.ini File Configuration (Bonus):** Start a quiz directly using settings from `quiz.ini`.
*   **Immediate Feedback & Score Saving:** Configurable options for quiz behavior.
*   **Modern, Cross-Platform UI:** Built with Avalonia for potential deployment on Windows, macOS, and Linux.

## 4. Project Structure (`SourceCode/GeographyQuizAvalonia/`)

The application is organized within a Visual Studio solution (`SourceCode/GeographyQuizAvalonia.sln`) containing a single C# Avalonia project (`GeographyQuizAvalonia.csproj`).

*   **Root Project Files:**
    *   `GeographyQuizAvalonia.csproj`: MSBuild project file (.NET SDK style).
    *   `Program.cs`: Main entry point, Avalonia app initialization.
    *   `App.axaml` & `App.axaml.cs`: Avalonia application definition, global styles (FluentTheme), DataTemplates (ViewLocator).
    *   `ViewLocator.cs`: Maps ViewModels to Views.
    *   `quiz.ini`: Sample configuration file, copied to output.
*   `Assets/`:
    *   `Flags/`: Contains placeholder flag images (e.g., `DE.png`). These are embedded resources.
    *   (Potential for other assets like icons, fonts).
*   `Converters/`: Contains XAML `IValueConverter` implementations (e.g., `BitmapAssetValueConverter`, `IsNotNullOrEmptyConverter`).
*   `Models/`: Contains POCO classes (`Country`, `Player`, `QuizSession`, `Question`).
*   `Services/`: Contains business logic and data access layers.
    *   `QuizManagerService.cs`: Core quiz operations.
    *   Repository classes (`CountryRepository`, `PlayerRepository`, `QuizSessionRepository`): Handle data operations (currently simulated).
    *   `DatabaseConnector.cs`: Manages database connection string (simulated).
    *   `IniConfigParser.cs`: Parses `quiz.ini`.
    *   `QuizTypeEnum.cs`.
*   `ViewModels/`:
    *   `ViewModelBase.cs`: Base class implementing `INotifyPropertyChanged`.
    *   `MainWindowViewModel.cs`: Manages current view in the main window.
    *   `MainViewModel.cs`, `QuizViewModel.cs`, `HighScoresViewModel.cs`: ViewModels for respective views.
*   `Views/`: Contains Avalonia XAML views (`.axaml`) and their code-behind (`.axaml.cs`).
    *   `MainWindow.axaml`: The main application window.
    *   `MainView.axaml`, `QuizView.axaml`, `HighScoresView.axaml`: UserControls for different application sections.

**Documentation Files:**
Located in the `Planungsdokumente/` folder (sibling to `SourceCode/`), these include ERD, SQL Schema, Class Diagrams, UI Mockups (from original design), and Test Plan.

## 5. Setup and Installation

This version uses **simulated data** and does not require a live MySQL database to run.

**Prerequisites:**
*   .NET 8.0 SDK (or newer compatible).
*   Microsoft Visual Studio 2022 (with .NET desktop development or Avalonia workloads) or JetBrains Rider.

**How to Run:**

1.  **Clone/Download the Repository.**
2.  **Open the Solution:**
    *   Navigate to the `SourceCode/` directory.
    *   Open `GeographyQuizAvalonia.sln` with Visual Studio or JetBrains Rider.
3.  **Build the Solution:**
    *   In your IDE, build the solution (e.g., Build > Build Solution or Ctrl+Shift+B in VS). This will compile the C# code, process XAML, and copy necessary files (like `quiz.ini` and flag assets) to the output directory (e.g., `SourceCode/GeographyQuizAvalonia/bin/Debug/net8.0/`).
    *   NuGet packages (Avalonia, etc.) should be restored automatically.
4.  **Run the Application:**
    *   Press F5 in Visual Studio (Start Debugging) or use the equivalent in Rider.
    *   Alternatively, navigate to the output directory and run `GeographyQuizAvalonia.exe` (on Windows) or execute `dotnet GeographyQuizAvalonia.dll`.

**Note on Simulated Data:**
The Data Access Layer (DAL) repositories currently return hardcoded sample data or use in-memory lists. No actual database connection is made. Console logs (viewable in your IDE's Output/Run window) indicate where database interactions would occur in a live setup. Placeholder flag images in `Assets/Flags/` are embedded and used by the application.

**For Future Real Database Integration (Conceptual):**
1.  Set up a MySQL database using the schema in `Planungsdokumente/RelationalSchema.sql`.
2.  Populate the `Countries` table with data, ensuring flag image paths are correct.
3.  Install the `MySql.Data` (or `MySqlConnector`) NuGet package.
4.  Update `Services/DatabaseConnector.cs` to use a real connection string (e.g., from a configuration file like `appsettings.json`, which is more common in .NET Core/8 than `App.config` for connection strings).
5.  Modify the repository classes in `Services/` to perform actual database operations using the chosen MySQL connector.

## 6. Using the Application

1.  **Main View:**
    *   Enter your name.
    *   Select a quiz mode, continent, and number of questions.
    *   Click "Start Quiz".
    *   Optionally, click "Start from Config" if `quiz.ini` is set up as desired.
    *   Click "View High Scores".
2.  **Quiz View:**
    *   The question (text and/or flag image) will be displayed.
    *   Click one of the answer buttons.
    *   Feedback is shown. The quiz proceeds until all questions are answered.
    *   A final score summary is displayed. Scores are saved (if enabled).
3.  **High Scores View:**
    *   View scores. Use radio buttons to filter. Click "Close" to return.

This application provides a solid foundation for a modern, cross-platform geography quiz. The next phase would involve significant UI/UX design enhancements.
