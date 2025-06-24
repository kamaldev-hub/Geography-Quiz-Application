Project Plan: Geography Quiz Software
1. Project Overview

The objective is to create a desktop application using C# (Windows Forms) and a MySQL database. The application will serve as a geography quiz platform where users can learn and test their knowledge of countries, capitals, and flags. The project must be completed within the specified timeframe (hypothetical: 23.06.2025 – 27.06.2025, submission by 31.01.2025) and must meet all minimum and bonus requirements outlined in the project specification.

Core Goal: Develop a robust, user-friendly quiz application with a clear separation of concerns (UI, business logic, data access), persistent high scores, and comprehensive documentation.

2. Core Technologies

Programming Language: C#

Framework: .NET Framework (for Windows Forms)

UI: Windows Forms

Database: MySQL

IDE: Microsoft Visual Studio

MySQL Connector: MySql.Data NuGet package

3. Phase 1: Planning and Design (Fulfilling "Planungsdokumente" Requirement)

This phase is critical and its outputs must be included in the final submission.

3.1. Database Design
3.1.1. Entity-Relationship Diagram (ERD)

Create an ERD with the following entities and relationships:

Countries: Stores all geographical data.

Players: Stores user information.

QuizSessions: Stores records of each quiz played.

Entities and Attributes:

Players

PlayerID (PK, INT, AUTO_INCREMENT)

PlayerName (VARCHAR(50), UNIQUE)

CreatedAt (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)

Countries

CountryID (PK, INT, AUTO_INCREMENT)

Name (VARCHAR(100), NOT NULL)

Capital (VARCHAR(100), NOT NULL)

Continent (ENUM('Asia', 'Europe', 'Africa', 'North America', 'South America', 'Oceania'), NOT NULL)

FlagImagePath (VARCHAR(255), NOT NULL) - Path to the flag image file relative to the application executable.

QuizSessions

SessionID (PK, INT, AUTO_INCREMENT)

PlayerID (FK to Players.PlayerID)

Score (INT, NOT NULL)

QuizTimestamp (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)

TotalQuestions (INT, NOT NULL)

QuizType (VARCHAR(50), NOT NULL) - e.g., "Country from Flag"

Relationship:

A Player can have many QuizSessions (One-to-Many).

3.1.2. Relational Data Model (SQL Schema)

Generate the CREATE TABLE scripts based on the ERD.

Generated sql
-- Create the database
CREATE DATABASE IF NOT EXISTS `geography_quiz_db`;
USE `geography_quiz_db`;

-- Table for Players
CREATE TABLE `Players` (
  `PlayerID` INT NOT NULL AUTO_INCREMENT,
  `PlayerName` VARCHAR(50) NOT NULL,
  `CreatedAt` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PlayerID`),
  UNIQUE INDEX `PlayerName_UNIQUE` (`PlayerName` ASC) VISIBLE);

-- Table for Countries
CREATE TABLE `Countries` (
  `CountryID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Capital` VARCHAR(100) NOT NULL,
  `Continent` ENUM('Asia', 'Europe', 'Africa', 'North America', 'South America', 'Oceania') NOT NULL,
  `FlagImagePath` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`CountryID`));

-- Table for Quiz Sessions (Highscores)
CREATE TABLE `QuizSessions` (
  `SessionID` INT NOT NULL AUTO_INCREMENT,
  `PlayerID` INT NOT NULL,
  `Score` INT NOT NULL,
  `QuizTimestamp` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  `TotalQuestions` INT NOT NULL,
  `QuizType` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`SessionID`),
  INDEX `FK_Player_idx` (`PlayerID` ASC) VISIBLE,
  CONSTRAINT `FK_Player`
    FOREIGN KEY (`PlayerID`)
    REFERENCES `Players` (`PlayerID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

3.2. Application Architecture

Implement a 3-Tier Architecture to ensure separation of concerns as required.

Presentation Layer (UI): Windows Forms. Responsible for displaying data and capturing user input. It should contain no business logic.

Business Logic Layer (BLL): Contains the core application logic (managing the quiz flow, creating questions, calculating scores). It acts as a bridge between the UI and DAL.

Data Access Layer (DAL): Responsible for all communication with the MySQL database (CRUD operations).

3.3. Class Diagram

Create a class diagram reflecting the 3-tier architecture.

DAL Namespace:

DatabaseConnector: Manages the MySQL connection string and provides a MySqlConnection object.

CountryRepository: Methods like GetAllCountries(), GetCountriesByContinent(string continent).

PlayerRepository: Methods like GetOrCreatePlayer(string name), GetAllPlayers().

QuizSessionRepository: Methods like SaveSession(QuizSession session), GetScoresByPlayer(int playerId), GetAllScores().

BLL Namespace:

Models (sub-namespace):

Player: { PlayerID, PlayerName }

Country: { CountryID, Name, Capital, Continent, FlagImagePath }

QuizSession: { SessionID, Player, Score, QuizTimestamp, ... }

Question: { QuestionText, CorrectAnswer, Options (List<string>), QuestionImage (Bitmap) }

QuizType (Enum): { CountryFromFlag, CapitalFromCountry, etc. }

QuizManager: The core logic class.

Properties: CurrentPlayer, CurrentQuizType, Questions (List<Question>), CurrentQuestionIndex, Score.

Methods: StartQuiz(QuizType type, string continent, int questionCount), GetNextQuestion(), SubmitAnswer(string answer), SaveResult().

UI Namespace (Windows Forms):

frmMain: The startup form. Allows player name entry, quiz selection, and navigation to high scores.

frmQuiz: The main quiz window. Displays the question, answer options, and provides immediate feedback.

frmHighScores: Displays quiz results in a DataGridView, with filtering options.

3.4. UI/UX Mockups (Prototyping)

Create simple visual mockups for each form to guide the implementation.

frmMain (Main Menu):

Label: "Enter Your Name:"

TextBox: txtPlayerName

GroupBox: "Select Quiz Mode" with 6 RadioButton controls for the different quiz types.

Label: "Filter by Continent (Optional):"

ComboBox: cmbContinent (populated with "Worldwide" and all continents).

Button: btnStartQuiz

Button: btnHighScores

Button: btnStartFromConfig (for bonus requirement)

frmQuiz (Quiz Window):

Label: lblQuestionNumber (e.g., "Question 5 / 10")

Label: lblScore (e.g., "Score: 40")

PictureBox: picQuestionImage (Visible for flag-related questions).

Label: lblQuestionText (Visible for text-based questions).

Button: btnOption1

Button: btnOption2

Button: btnOption3

Button: btnOption4

Label: lblFeedback (e.g., "Correct!" or "Wrong! The answer was..."). Initially invisible.

ProgressBar: progressBarQuiz

frmHighScores (High Scores Window):

DataGridView: dgvHighScores to display columns: Player Name, Score, Date, Quiz Type.

RadioButton: rbAllPlayers

RadioButton: rbMyScores (Enabled only if a player has played).

Button: btnClose

4. Phase 2: Development and Implementation

Follow a step-by-step approach, building the application layer by layer.

4.1. Project Setup

Create a new C# Windows Forms App (.NET Framework) project in Visual Studio.

Install the MySql.Data NuGet package.

Organize the project into folders: DAL, BLL, UI, Models, Resources (for flags), Config.

Add a App.config file and store the MySQL connection string there.

4.2. Data Population

Crucial Step: Source a dataset of countries, capitals, and continents. A CSV or JSON file is ideal.

Source a complete set of world flags (e.g., PNG format). Name them consistently (e.g., DE.png, FR.png, US.png using ISO 3166-1 alpha-2 codes).

Create a sub-folder Resources/Flags in the project output directory and place all flag images there.

Write a one-time utility script (or a small console app) to read the dataset and populate your Countries table in the MySQL database, including the relative path to the flag image (Flags/DE.png).

4.3. Implementing the Data Access Layer (DAL)

Create the repository classes (CountryRepository, PlayerRepository, QuizSessionRepository).

Implement methods using MySqlConnection, MySqlCommand, and MySqlDataReader.

Important: Use parameterized queries (@parameterName) to prevent SQL injection.

Wrap database connections in using blocks to ensure they are properly closed and disposed of.

Handle potential MySqlException errors gracefully.

4.4. Implementing the Business Logic Layer (BLL)

Implement the Models classes (Player, Country, etc.) as simple Plain Old C# Objects (POCOs).

Implement the QuizManager class. This is the core of the application.

StartQuiz method:

Takes quiz type, continent filter, and number of questions as input.

Calls the CountryRepository to fetch the relevant list of countries.

Randomly selects the specified number of unique countries for the questions.

For each selected country, it generates a Question object:

Determine the question (FlagImagePath or Name) and the correct answer (Name or Capital).

Randomly select 3 other incorrect answers from the master list of countries. Ensure they are unique and not the correct answer.

Shuffle the 4 answer options.

Store the list of generated Question objects.

SubmitAnswer method: Compares the user's selected answer with the correct answer for the current question, updates the score, and returns a boolean isCorrect.

SaveResult method: Creates a QuizSession object and passes it to the QuizSessionRepository to be saved in the database.

4.5. Implementing the Presentation Layer (UI)

Build the forms as designed in the mockups.

frmMain Logic:

On load, populate the continent ComboBox.

btnStartQuiz_Click:

Validate that a player name is entered.

Call PlayerRepository.GetOrCreatePlayer() to get the current player object.

Instantiate frmQuiz, passing the selected quiz type, continent, and player object.

Show frmQuiz as a modal dialog.

frmQuiz Logic:

In the constructor, receive the quiz parameters and instantiate a QuizManager.

Call quizManager.StartQuiz() and then call a local method DisplayQuestion().

DisplayQuestion method:

Gets the current question from quizManager.GetNextQuestion().

Updates all UI controls: lblQuestionNumber, lblQuestionText, picQuestionImage, and the text of the four answer buttons.

Resets feedback label and button colors.

Answer Button Click Event (shared by all 4 buttons):

Call quizManager.SubmitAnswer() with the clicked button's text.

Display immediate feedback ("Correct!" in green, "Wrong..." in red).

Disable answer buttons briefly.

Use a Timer or async Task.Delay to pause for 1-2 seconds before loading the next question or ending the quiz.

If it's the last question, show a summary, save the result via quizManager.SaveResult(), and close the form.

frmHighScores Logic:

On load, fetch and display all scores by default.

Implement the CheckedChanged event for the radio buttons to refetch and display either all scores or scores for the current player. Use the DataGridView.DataSource property for easy data binding.

4.6. Implementing Bonus Requirements (18 Points)

Continent Filtering: This is already integrated into the design. The StartQuiz method in the QuizManager accepts a continent string. If the string is "Worldwide" or null, fetch all countries; otherwise, call CountryRepository.GetCountriesByContinent().

.ini File Configuration:

Create a quiz.ini file in the project's root directory and set its "Copy to Output Directory" property to "Copy if newer".

Example quiz.ini:

Generated ini
[QuizConfig]
fragetyp = flagge_zu_land
kontinent = europa
anzahl_fragen = 10
feedback_anzeigen = ja
punkte_speichern = ja
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
Ini
IGNORE_WHEN_COPYING_END

Create a simple INI parser class or use a small library. A basic parser can read the file line by line, split on =, and store key-value pairs in a Dictionary<string, string>.

Implement the btnStartFromConfig_Click event handler on frmMain:

Read and parse quiz.ini.

Map the string values (e.g., "flagge_zu_land") to your QuizType enum.

Validate the config values.

Directly instantiate and show frmQuiz with the loaded configuration, bypassing the manual selection controls. Handle the feedback_anzeigen and punkte_speichern flags within the quiz logic.

5. Phase 3: Testing and Quality Assurance

Create a Test Plan: Document test cases for all functionalities.

Functional Testing:

Verify all 6 quiz modes work correctly.

Test the continent filter for each continent and "Worldwide".

Confirm that 10 unique questions are generated.

Confirm that 4 unique answer options are generated for each question.

Verify immediate feedback is accurate.

Verify score calculation and saving.

Test the high score display and filtering (All vs. My Scores).

Test the .ini file configuration.

Error Handling:

Test what happens if the database connection fails. The app should show a user-friendly error message, not crash.

Test with invalid input (e.g., empty player name).

Test what happens if flag images are missing. The app should handle this gracefully (e.g., show a placeholder image).

Usability Testing: Ensure the UI is intuitive and easy to navigate.

6. Phase 4: Documentation and Final Delivery
6.1. Code Documentation

Comments: Add XML comments (///) to all public classes and methods in the BLL and DAL to explain their purpose, parameters, and return values.

Code Formatting: Ensure consistent and clean formatting throughout the codebase.

README.md: Create a README.md file in the root of the solution. It should contain:

Project title and brief description.

Setup Instructions:

Prerequisites (Visual Studio, .NET Framework, MySQL Server).

How to create the database and tables using the provided SQL script.

How to configure the App.config connection string.

Instructions on where to place the Resources/Flags folder.

How to Run: Simple instructions on building and running the project from Visual Studio.

6.2. Final Submission Package

As per the requirements, create a single digital submission (e.g., a .zip file) containing:

Planungsdokumente Folder:

ER Diagram (as an image or PDF).

Relational Schema (the .sql script).

Class Diagram (as an image or PDF).

UI Mockups (images).

(Optional but recommended) Flowcharts or Pseudocode for the QuizManager.StartQuiz method.

SourceCode Folder:

The complete Visual Studio Solution folder, including the quiz.ini file. Ensure it can be opened and built directly.

InstallationAnleitung.md (or README.md): The detailed setup and installation guide created in the previous step.

This comprehensive plan ensures that every requirement from the specification is addressed, resulting in a high-quality, well-documented, and fully functional application.
