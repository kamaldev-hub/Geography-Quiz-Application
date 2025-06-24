# Geography Quiz Application

## 1. Project Overview

The Geography Quiz Application is a C# Windows Forms desktop program designed to test and enhance users' knowledge of world geography, including countries, capitals, and flags. It features multiple quiz modes, configurable quiz sessions, and high score tracking.

This project was developed based on the specifications outlined in `AGENTS.md`.

## 2. Core Technologies

*   **Programming Language:** C#
*   **Framework:** .NET Framework (Target: v4.7.2)
*   **UI:** Windows Forms
*   **Database (Conceptual):** MySQL (The application is built with a DAL for MySQL, but for this version, database interactions are simulated.)
*   **IDE:** Microsoft Visual Studio

## 3. Features

*   **Multiple Quiz Modes:**
    *   Country from Flag
    *   Capital from Country
    *   Flag from Country
    *   Country from Capital
    *   Capital from Flag
    *   Flag from Capital
*   **Player Management:** Enter player name to track scores.
*   **Continent Filtering:** Filter quizzes by specific continents or play "Worldwide".
*   **Configurable Quiz Length:** Choose the number of questions (5-20).
*   **High Score Tracking:** View local high scores, filter by "All Players" or "My Scores".
*   **.ini File Configuration (Bonus):** Start a quiz directly using settings from a `quiz.ini` file.
*   **Immediate Feedback:** Optional feedback after each question.
*   **Score Saving:** Optional saving of scores.

## 4. Project Structure

The application is organized within a Visual Studio solution (`GeographyQuizApp.sln`) containing a single C# Windows Forms project (`GeographyQuizApp.csproj`). The project follows a 3-Tier Architecture:

*   **Root Project Files:**
    *   `GeographyQuizApp.csproj`: The MSBuild project file defining structure, references, and build settings.
    *   `Program.cs`: Contains the `Main` entry point for the application.
    *   `App.config`: Contains application settings, including the (placeholder) MySQL connection string.
    *   `quiz.ini`: Sample configuration file for the bonus feature, copied to output directory.
    *   `README.md`: This file.
*   `Properties/`:
    *   `AssemblyInfo.cs`: Contains assembly metadata (version, title, etc.).
*   `UI/`: Presentation Layer (Windows Forms)
    *   `frmMain.cs`: Main menu and quiz setup.
    *   `frmQuiz.cs`: Active quiz gameplay.
    *   `frmHighScores.cs`: Displays high scores.
    *   Associated `.Designer.cs` and `.resx` files for each form.
*   `BLL/`: Business Logic Layer
    *   `Models/`: Contains POCO classes (`Player`, `Country`, `QuizSession`, `Question`).
    *   `QuizManager.cs`: Core quiz operations, question generation, scoring.
    *   `QuizTypeEnum.cs`: Enumeration for different quiz types.
    *   `IniConfigParser.cs`: Parses the `quiz.ini` file.
*   `DAL/`: Data Access Layer
    *   `DatabaseConnector.cs`: Manages database connection string (simulated connection).
    *   `CountryRepository.cs`: Handles data operations for countries (simulated).
    *   `PlayerRepository.cs`: Handles data operations for players (simulated).
    *   `QuizSessionRepository.cs`: Handles data operations for quiz sessions/high scores (simulated).
*   `Resources/Flags/`: This directory contains placeholder flag images (e.g., `DE.png`, `US.png`), which are copied to the output directory. For full visual representation, replace these placeholders with actual flag images.
*   `Config/`: (Currently unused, but designated for future configuration files).

The main solution file `GeographyQuizApp.sln` is located in the parent directory (`SourceCode/`).
Documentation files (ERD, Schema, Class Diagram, Mockups, Test Plan) are located in the `Planungsdokumente` folder (typically in the root of the overall submission, alongside the `SourceCode` folder).

## 5. Setup and Installation

This version uses simulated data and does not require a live MySQL database to run.

**Prerequisites:**
*   Microsoft Visual Studio (2017 or later recommended, with .NET desktop development workload installed).
*   .NET Framework 4.7.2 (Developer Pack).

**Steps for Full Database Setup (Conceptual - for future extension):**

If you wish to connect to a real MySQL database in the future:
1.  **Database Creation:**
    *   Using a MySQL management tool, connect to your MySQL server.
    *   Execute the SQL script found in `Planungsdokumente/RelationalSchema.sql`. This will create the `geography_quiz_db` database and the required tables.
2.  **Populate `Countries` Table:**
    *   Source a dataset of countries, capitals, continents.
    *   Source actual flag images (e.g., PNG format, named like `DE.png`, `FR.png` using ISO 3166-1 alpha-2 codes). Replace the placeholder images in `Resources/Flags/` with these actual images. The `.csproj` is already set to copy them.
    *   A utility script (not provided) would be needed to populate the `Countries` table, ensuring `FlagImagePath` correctly points to `Flags/YourFlag.png`.
3.  **Install `MySql.Data` NuGet Package:**
    *   The `.csproj` file includes a commented-out reference to `MySql.Data`. To use a real database, uncomment this reference or, preferably, install the `MySql.Data` NuGet package via Visual Studio's NuGet Package Manager.
4.  **Configure Connection String:**
    *   Open `App.config` in Visual Studio.
    *   Locate the `MySqlConnectionString` appSetting and replace `"YOUR_MYSQL_CONNECTION_STRING_HERE"` with your actual MySQL connection string (e.g., `Server=localhost;Port=3306;Database=geography_quiz_db;Uid=your_user;Pwd=your_password;`).

## 6. How to Run

1.  **Open the Solution:**
    *   Navigate to the `SourceCode/` directory.
    *   Open `GeographyQuizApp.sln` with Visual Studio.
2.  **Build the Solution:**
    *   In Visual Studio, build the solution (Build > Build Solution or Ctrl+Shift+B). This will compile the C# code and copy necessary files (like `quiz.ini` and flag images) to the output directory (e.g., `SourceCode/GeographyQuizApp/bin/Debug/`).
    *   The `MySql.Data` NuGet package is referenced in the `.csproj` but is not strictly required for the current simulated DAL to function. If building for real database use, ensure it's properly installed.
3.  **Run the Application:**
    *   Press F5 in Visual Studio (Start Debugging) or Ctrl+F5 (Start Without Debugging).
    *   Alternatively, navigate to the output directory (e.g., `SourceCode/GeographyQuizApp/bin/Debug/`) and run `GeographyQuizApp.exe` directly.

**Note on Simulated Data:** The current Data Access Layer (DAL) repositories return hardcoded sample data or use in-memory lists. No actual database connection is made. Console logs (viewable in Visual Studio's Output window during debugging) indicate where database interactions would occur in a live setup. The placeholder flag images in `Resources/Flags/` are copied to the output and will be displayed if not replaced with actual images.

## 7. Using the Application

1.  **Main Menu (`frmMain`):**
    *   Enter your name.
    *   Select a quiz mode.
    *   Optionally, choose a continent filter.
    *   Set the number of questions.
    *   Click "Start Quiz".
    *   Alternatively, click "Start from Config" if `quiz.ini` is set up.
    *   Click "View High Scores" to see past results.
2.  **Quiz Window (`frmQuiz`):**
    *   The question (text and/or flag image) will be displayed.
    *   Click one of the four answer buttons.
    *   Feedback will be shown (if enabled).
    *   The quiz proceeds until all questions are answered.
    *   A final score summary is displayed. Scores are saved (if enabled).
3.  **High Scores Window (`frmHighScores`):**
    *   View scores. Use radio buttons to filter between "All Players" and "My Scores" (if your name was entered).
    *   Click "Close" to return.

This application aims to be a comprehensive and enjoyable geography learning tool.
