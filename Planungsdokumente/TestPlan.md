# Geography Quiz Application - Test Plan

## 1. Introduction

This document outlines the test plan for the Geography Quiz Application. The purpose of this testing phase is to ensure the application meets the specified requirements, is user-friendly, and robust.

## 2. Scope of Testing

*   **Functional Testing:** Verifying all application features work as intended.
*   **Usability Testing:** Assessing the ease of use and intuitiveness of the UI.
*   **Error Handling:** Testing how the application handles invalid inputs and unexpected situations (e.g., database connection issues, missing files).
*   **Configuration Testing:** Testing the `.ini` file configuration feature.

## 3. Test Strategy

Testing will be performed manually in a simulated environment that mimics the target user setup (.NET Framework, conceptual MySQL database, flag images in specified location).

## 4. Test Cases

### 4.1. Functional Testing

**4.1.1. Main Menu (frmMain)**
*   **TC-FN-001:** Player Name Entry:
    *   Verify player name can be entered.
    *   Verify quiz cannot start if player name is empty (shows warning).
    *   Verify player is created/retrieved in the (simulated) database.
*   **TC-FN-002:** Quiz Mode Selection:
    *   Verify all 6 quiz mode radio buttons are selectable.
    *   Verify only one quiz mode can be selected at a time.
*   **TC-FN-003:** Continent Filter Selection:
    *   Verify "Worldwide" is the default.
    *   Verify all specified continents can be selected from the dropdown.
*   **TC-FN-004:** Number of Questions Selection:
    *   Verify default is 10.
    *   Verify min (5) and max (20) limits are enforced.
*   **TC-FN-005:** Start Quiz Button:
    *   Verify clicking "Start Quiz" with valid inputs launches `frmQuiz`.
    *   Verify correct parameters (quiz type, continent, question count, player) are passed to `QuizManager`.
*   **TC-FN-006:** View High Scores Button:
    *   Verify clicking "View High Scores" launches `frmHighScores`.
*   **TC-FN-007:** Start from Config Button:
    *   Verify button is enabled only if `quiz.ini` exists.
    *   Verify clicking it launches `frmQuiz` with parameters from `quiz.ini`.
    *   Test with valid `quiz.ini` values.
    *   Test with `quiz.ini` having some missing/invalid values (graceful handling expected).

**4.1.2. Quiz Window (frmQuiz)**
*   **TC-FN-008:** Question Display:
    *   For each of the 6 quiz types:
        *   Verify question text is appropriate.
        *   Verify flag image is displayed if applicable (and path is correct).
        *   Verify 4 unique answer options are displayed.
        *   Verify one option is the correct answer.
*   **TC-FN-009:** Answer Submission:
    *   Verify clicking an answer option registers the answer.
    *   Verify immediate feedback (if `feedback_anzeigen=ja` or manual start) is shown:
        *   "Correct!" in green for correct answers.
        *   "Wrong! The correct answer was..." in red for incorrect answers.
    *   Verify answer buttons are briefly disabled/highlighted during feedback.
*   **TC-FN-010:** Scoring:
    *   Verify score updates correctly (+10 for correct, +0 for incorrect).
    *   Verify score is displayed accurately.
*   **TC-FN-011:** Question Navigation:
    *   Verify questions proceed automatically after feedback delay (if applicable).
    *   Verify progress bar updates with each question.
    *   Verify question counter (`X / Y`) updates.
*   **TC-FN-012:** Quiz Completion:
    *   Verify quiz ends after the specified number of questions.
    *   Verify final score summary is shown.
    *   Verify score is saved to (simulated) database if `punkte_speichern=ja` or manual start.
    *   Verify form closes.
*   **TC-FN-013:** Continent Filtering in Quiz Logic:
    *   Select a specific continent (e.g., "Europe") and 10 questions. Start quiz.
    *   Manually verify (through console logs or by knowing the test data) that all questions are based on European countries.
    *   Repeat for "Worldwide".
*   **TC-FN-014:** Unique Questions and Options:
    *   Start a quiz with 10 questions. Verify all 10 questions are unique (target different countries/capitals/flags).
    *   For each question, verify all 4 answer options are unique.

**4.1.3. High Scores Window (frmHighScores)**
*   **TC-FN-015:** Display All Scores:
    *   Verify "All Players" is default.
    *   Verify DataGridView populates with all scores from (simulated) database, ordered by Score (desc) then Date (desc).
    *   Verify columns: Player Name, Score, Date, Quiz Type, Questions.
*   **TC-FN-016:** Display My Scores:
    *   Verify "My Scores" radio button is enabled only if a player name was active on `frmMain`.
    *   If selected, verify DataGridView filters to show only scores for the current player.
*   **TC-FN-017:** Close Button:
    *   Verify "Close" button closes `frmHighScores`.

### 4.2. Usability Testing
*   **TC-US-001:** Navigation: Is the flow between forms intuitive?
*   **TC-US-002:** Clarity: Are labels, instructions, and feedback messages clear and understandable?
*   **TC-US-003:** Layout: Is the layout of controls on each form logical and uncluttered?
*   **TC-US-004:** Responsiveness: Does the application respond promptly to user actions (simulated)?

### 4.3. Error Handling
*   **TC-ER-001:** Missing `quiz.ini`:
    *   If `quiz.ini` is missing, verify "Start from Config" button is disabled or handles it gracefully with a message.
*   **TC-ER-002:** Invalid `quiz.ini` content:
    *   Test with malformed `quiz.ini` (e.g., incorrect keys, non-integer for `anzahl_fragen`). Verify user-friendly error message and no crash.
*   **TC-ER-003:** Missing Flag Images:
    *   Remove/rename a flag image file referenced by the (simulated) country data.
    *   Start a quiz that would use this flag.
    *   Verify the application doesn't crash. It should ideally show a placeholder or skip the question, with a console log. `frmQuiz` attempts to show "(Image unavailable)".
*   **TC-ER-004:** Database Connection Failure (Conceptual):
    *   If the (simulated) `DatabaseConnector` were to indicate a failure, verify other parts of the application (e.g., repositories) handle this by logging an error and returning empty data/false, preventing crashes. User should see a message if data cannot be loaded/saved.
*   **TC-ER-005:** Insufficient Data for Quiz:
    *   If selected criteria (e.g., a continent with <4 countries, or total countries <4) results in too few countries to form questions/options, verify `QuizManager.StartQuiz` handles this gracefully and `frmMain` informs the user.

## 5. Test Environment (Conceptual)

*   **OS:** Windows
*   **.NET Framework:** Version 4.7.2 (as per App.config)
*   **Database:** MySQL (Simulated - interactions logged to console, repositories return mock data)
*   **Flag Images:** PNG files in `Resources/Flags` folder relative to executable.
*   **IDE for running:** Visual Studio (simulated)

## 6. Test Deliverables

*   This Test Plan document.
*   (If applicable in a real scenario) Test Execution Log with results of each test case.
*   (If applicable in a real scenario) Bug reports for failed test cases.

This Test Plan provides a conceptual outline for testing. Actual testing would involve executing these steps and observing behavior, including console output for simulated backend operations.
