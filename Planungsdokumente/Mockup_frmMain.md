**Form: frmMain (Main Menu)**

*   **Title:** Geography Quiz - Main Menu
*   **Controls:**
    *   `lblPlayerNamePrompt` (Label): "Enter Your Name:"
        *   Text: "Enter Your Name:"
    *   `txtPlayerName` (TextBox)
        *   AccessibleName: "Player Name Input"
    *   `grpQuizMode` (GroupBox): "Select Quiz Mode"
        *   Text: "Select Quiz Mode"
        *   Controls within GroupBox:
            *   `rbCountryFromFlag` (RadioButton): "Country from Flag" (Checked by default)
            *   `rbCapitalFromCountry` (RadioButton): "Capital from Country"
            *   `rbFlagFromCountry` (RadioButton): "Flag from Country"
            *   `rbCountryFromCapital` (RadioButton): "Country from Capital"
            *   `rbCapitalFromFlag` (RadioButton): "Capital from Flag"
            *   `rbFlagFromCapital` (RadioButton): "Flag from Capital"
    *   `lblContinentFilterPrompt` (Label): "Filter by Continent (Optional):"
        *   Text: "Filter by Continent (Optional):"
    *   `cmbContinent` (ComboBox)
        *   AccessibleName: "Continent Filter"
        *   Items: "Worldwide", "Asia", "Europe", "Africa", "North America", "South America", "Oceania"
        *   Default: "Worldwide"
    *   `lblNumberOfQuestionsPrompt` (Label): "Number of Questions:"
        *   Text: "Number of Questions:"
    *   `numNumberOfQuestions` (NumericUpDown)
        *   AccessibleName: "Number of Questions"
        *   Value: 10
        *   Minimum: 5
        *   Maximum: 20
    *   `btnStartQuiz` (Button)
        *   Text: "Start Quiz"
        *   AccessibleName: "Start Quiz Button"
    *   `btnHighScores` (Button)
        *   Text: "View High Scores"
        *   AccessibleName: "View High Scores Button"
    *   `btnStartFromConfig` (Button)
        *   Text: "Start from Config File"
        *   AccessibleName: "Start from Config File Button"

**Layout Notes:**

*   Player name entry at the top.
*   Quiz mode selection in a group box below it.
*   Continent filter and number of questions below quiz modes.
*   Action buttons (Start, High Scores, Start from Config) at the bottom.
*   Consider a visually appealing layout, perhaps using Panels or FlowLayoutPanels for organization.
