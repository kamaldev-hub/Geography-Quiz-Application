**Form: frmQuiz (Quiz Window)**

*   **Title:** Geography Quiz - In Progress
*   **Controls:**
    *   `lblQuestionNumber` (Label)
        *   Text: "Question: 1 / 10" (Example)
        *   AccessibleName: "Question Progress"
    *   `lblScore` (Label)
        *   Text: "Score: 0" (Example)
        *   AccessibleName: "Current Score"
    *   `picQuestionImage` (PictureBox)
        *   Visible: True (for flag-related questions, otherwise potentially hidden or shows a generic image)
        *   SizeMode: Zoom
        *   AccessibleName: "Question Image (Flag)"
        *   (Placeholder for image)
    *   `lblQuestionText` (Label)
        *   Text: "Which country does this flag belong to?" / "What is the capital of [Country]?" (Example)
        *   Visible: True (for text-based questions, or in addition to image)
        *   MaximumSize: (Set to ensure text wraps and doesn't overflow)
        *   AutoSize: False
        *   TextAlign: MiddleCenter
        *   AccessibleName: "Question Text"
    *   `btnOption1` (Button)
        *   Text: "Option A" (Example answer)
        *   AccessibleName: "Answer Option 1"
    *   `btnOption2` (Button)
        *   Text: "Option B" (Example answer)
        *   AccessibleName: "Answer Option 2"
    *   `btnOption3` (Button)
        *   Text: "Option C" (Example answer)
        *   AccessibleName: "Answer Option 3"
    *   `btnOption4` (Button)
        *   Text: "Option D" (Example answer)
        *   AccessibleName: "Answer Option 4"
    *   `lblFeedback` (Label)
        *   Text: "Correct!" / "Wrong! The correct answer was [CorrectAnswer]." (Example)
        *   Visible: False (initially)
        *   TextAlign: MiddleCenter
        *   AccessibleName: "Answer Feedback"
    *   `progressBarQuiz` (ProgressBar)
        *   Minimum: 0
        *   Maximum: 10 (corresponds to number of questions)
        *   Value: 1
        *   Style: Continuous or Blocks
        *   AccessibleName: "Quiz Progress Bar"
    *   `timerFeedback` (Timer)
        *   Interval: 1500 (1.5 seconds, for showing feedback before next question)
        *   Enabled: False

**Layout Notes:**

*   Question number and score typically at the top.
*   Question image/text prominently displayed in the center/upper part.
*   Answer buttons arranged below the question, likely 2x2 or 4x1.
*   Feedback label below answer buttons.
*   Progress bar at the very top or bottom.
*   The form should be modal.
*   Ensure buttons are large enough for easy clicking.
*   Feedback label text color should change (e.g., Green for correct, Red for wrong).
