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

The application follows a 3-Tier Architecture:

*   `UI/`: Presentation Layer (Windows Forms)
    *   `frmMain.cs`: Main menu and quiz setup.
    *   `frmQuiz.cs`: Active quiz gameplay.
    *   `frmHighScores.cs`: Displays high scores.
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
*   `Resources/Flags/`: This directory is intended to store flag images (e.g., `DE.png`, `US.png`). **Flag images are not included in this package and must be sourced separately.**
*   `Config/`: (Currently unused, but designated for future configuration files if App.config is not sufficient).
*   `App.config`: Contains application settings, including the (placeholder) MySQL connection string.
*   `quiz.ini`: Sample configuration file for the bonus feature.

Documentation files (ERD, Schema, Class Diagram, Mockups, Test Plan) are located in the `Planungsdokumente` folder (outside this `GeographyQuizApp` source folder, typically in the root of the overall submission).

## 5. Setup and Installation (Conceptual for Full Database Functionality)

This version uses simulated data and does not require a live MySQL database to run the basic application. For full functionality as designed:

**Prerequisites:**
*   Microsoft Visual Studio (2017 or later recommended, with .NET desktop development workload).
*   .NET Framework 4.7.2 (Developer Pack if building, Runtime if only running).
*   MySQL Server (e.g., v5.7 or v8.0).
*   MySQL Workbench or similar tool for database management (optional).

**Steps for Full Database Setup (Conceptual):**

1.  **Database Creation:**
    *   Using a MySQL management tool, connect to your MySQL server.
    *   Execute the SQL script found in `Planungsdokumente/RelationalSchema.sql`. This will create the `geography_quiz_db` database and the required tables (`Players`, `Countries`, `QuizSessions`).

2.  **Populate `Countries` Table:**
    *   A dataset of countries, capitals, continents, and flag image paths is required.
    *   Flag images (e.g., PNG format, named like `DE.png`, `FR.png` using ISO 3166-1 alpha-2 codes) must be placed in a `Resources/Flags` subfolder relative to the application's executable directory (e.g., `bin/Debug/Resources/Flags/`).
    *   A utility script (not provided in this package) would be needed to read your country dataset (e.g., from a CSV) and populate the `Countries` table in your MySQL database. The `FlagImagePath` column should store relative paths like `Flags/DE.png`.

3.  **Configure Connection String:**
    *   Open `GeographyQuizApp/App.config` in a text editor or Visual Studio.
    *   Locate the `MySqlConnectionString` appSetting:
        ```xml
        <add key="MySqlConnectionString" value="YOUR_MYSQL_CONNECTION_STRING_HERE" />
        ```
    *   Replace `"YOUR_MYSQL_CONNECTION_STRING_HERE"` with your actual MySQL connection string.
        *   Example: `Server=localhost;Port=3306;Database=geography_quiz_db;Uid=your_mysql_user;Pwd=your_mysql_password;`

## 6. How to Run (Simulated Version)

1.  **Open the Solution:**
    *   (Conceptually) Open the `GeographyQuizApp.sln` file (not generated in this environment, but would be present in a real VS project) in Visual Studio.
2.  **Build the Solution:**
    *   In Visual Studio, build the solution (Build > Build Solution). This will compile the C# code.
    *   The `MySql.Data` NuGet package would be restored automatically if it was listed in a packages.config or .csproj file (not explicitly managed in this agent environment, but assumed for real project).
3.  **Prepare `quiz.ini` (Optional Bonus Feature):**
    *   Ensure the `quiz.ini` file is present in the output directory (e.g., `bin/Debug/` or `bin/Release/`) alongside `GeographyQuizApp.exe`. If you added it to the project in Visual Studio, set its "Copy to Output Directory" property to "Copy if newer" or "Copy always". The provided `quiz.ini` is in the root of `GeographyQuizApp` folder.
4.  **Prepare Flag Images (For Visuals):**
    *   Create a folder named `Resources` in the output directory (e.g., `bin/Debug/`).
    *   Inside `Resources`, create a folder named `Flags`.
    *   Place your flag image files (e.g., `DE.png`, `FR.png`, etc.) into this `Resources/Flags/` folder. The application's simulated data expects these.
5.  **Run the Application:**
    *   Press F5 in Visual Studio, or run `GeographyQuizApp.exe` directly from the output directory (e.g., `bin/Debug/`).

**Note on Simulated Data:** The current DAL repositories (`CountryRepository`, `PlayerRepository`, `QuizSessionRepository`) return hardcoded sample data or use in-memory lists. No actual database connection is made in this provided version. Console logs indicate where database interactions would occur.

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
