**Form: frmHighScores (High Scores Window)**

*   **Title:** Geography Quiz - High Scores
*   **Controls:**
    *   `grpFilterScores` (GroupBox): "Filter Scores"
        *   `rbAllPlayers` (RadioButton)
            *   Text: "All Players"
            *   Checked: True (initially)
            *   AccessibleName: "Filter All Players Scores"
        *   `rbMyScores` (RadioButton)
            *   Text: "My Scores"
            *   Enabled: False (initially, until a player plays a game)
            *   AccessibleName: "Filter My Scores"
    *   `dgvHighScores` (DataGridView)
        *   AccessibleName: "High Scores Table"
        *   Columns:
            *   `PlayerName` (HeaderText: "Player Name")
            *   `Score` (HeaderText: "Score")
            *   `QuizTimestamp` (HeaderText: "Date")
            *   `QuizType` (HeaderText: "Quiz Type")
            *   `TotalQuestions` (HeaderText: "Questions")
        *   ReadOnly: True
        *   AllowUserToAddRows: False
        *   AllowUserToDeleteRows: False
        *   AutoSizeColumnsMode: Fill
    *   `btnClose` (Button)
        *   Text: "Close"
        *   AccessibleName: "Close High Scores Button"

**Layout Notes:**

*   Filter options (RadioButtons) at the top.
*   DataGridView taking up most of the form space below the filters.
*   Close button at the bottom.
*   The form should be modal.
*   Ensure `dgvHighScores` columns are clearly readable.
*   `rbMyScores` should only be enabled if there's a "current player" context from `frmMain` or if the player has previously played. For simplicity, it might be enabled if any scores exist for the player name entered in `frmMain`.
