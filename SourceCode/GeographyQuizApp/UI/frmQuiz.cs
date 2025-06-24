using GeographyQuizApp.BLL;
using GeographyQuizApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Drawing; // For Color
using System.IO;    // For Path, File
using System.Windows.Forms;

namespace GeographyQuizApp.UI
{
    public partial class frmQuiz : Form
    {
        private readonly QuizManager _quizManager;
        private Question? _currentQuestion;
        private readonly List<Button> _optionButtons;
        private readonly bool _showFeedbackImmediately;
        private readonly bool _savePoints;

        public frmQuiz(QuizManager quizManager, bool showFeedbackImmediately, bool savePoints)
        {
            InitializeComponent();
            _quizManager = quizManager ?? throw new ArgumentNullException(nameof(quizManager));
            _showFeedbackImmediately = showFeedbackImmediately;
            _savePoints = savePoints;

            _optionButtons = new List<Button> { btnOption1, btnOption2, btnOption3, btnOption4 };
            // Ensure panel is used if defined in designer for option buttons
            // For this example, buttons are directly on the form or in a panel pnlOptions
        }

        private void frmQuiz_Load(object sender, EventArgs e)
        {
            if (!_quizManager.IsQuizActive || _quizManager.Questions == null || _quizManager.Questions.Count == 0)
            {
                MessageBox.Show("Quiz could not be loaded. No questions available.", "Quiz Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            progressBarQuiz.Maximum = _quizManager.TotalQuestionsInQuiz;
            LoadNextQuestion();
        }

        private void LoadNextQuestion()
        {
            _currentQuestion = _quizManager.GetNextQuestion();
            timerFeedback.Stop(); // Stop timer if it was running for previous feedback

            if (_currentQuestion != null)
            {
                UpdateUIForQuestion();
            }
            else // Quiz finished
            {
                QuizFinished();
            }
        }

        private void UpdateUIForQuestion()
        {
            if (_currentQuestion == null) return;

            lblQuestionNumber.Text = $"Question: {_quizManager.CurrentQuestionIndex + 1} / {_quizManager.TotalQuestionsInQuiz}";
            lblScore.Text = $"Score: {_quizManager.Score}";
            progressBarQuiz.Value = _quizManager.CurrentQuestionIndex + 1;

            lblQuestionText.Text = _currentQuestion.QuestionText;

            // Handle image display
            if (!string.IsNullOrEmpty(_currentQuestion.ImagePath))
            {
                string fullImagePath = Path.Combine(Application.StartupPath, _currentQuestion.ImagePath);
                if (File.Exists(fullImagePath))
                {
                    try
                    {
                        picQuestionImage.Image = Image.FromFile(fullImagePath);
                        picQuestionImage.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading image: {fullImagePath} - {ex.Message}");
                        picQuestionImage.Image = null; // Clear previous image
                        picQuestionImage.Visible = false;
                        // Optionally show a placeholder or error image
                    }
                }
                else
                {
                    picQuestionImage.Image = null;
                    picQuestionImage.Visible = false;
                    Console.WriteLine($"Image not found: {fullImagePath}");
                    // Show placeholder or hide if image is crucial and missing
                    if (_quizManager.CurrentQuizType == QuizTypeEnum.CountryFromFlag ||
                        _quizManager.CurrentQuizType == QuizTypeEnum.CapitalFromFlag ||
                        _quizManager.CurrentQuizType == QuizTypeEnum.FlagFromCapital || // if flag is part of q
                        _quizManager.CurrentQuizType == QuizTypeEnum.FlagFromCountry) // if flag is part of q
                    {
                         lblQuestionText.Text += "\n (Image unavailable)";
                    }
                }
            }
            else
            {
                picQuestionImage.Visible = false;
            }

            // Populate answer buttons
            for (int i = 0; i < _optionButtons.Count; i++)
            {
                if (i < _currentQuestion.Options.Count)
                {
                    _optionButtons[i].Text = _currentQuestion.Options[i];
                    _optionButtons[i].Enabled = true;
                    _optionButtons[i].BackColor = SystemColors.Control; // Reset color
                    _optionButtons[i].Visible = true;
                }
                else
                {
                    _optionButtons[i].Visible = false; // Hide unused buttons
                }
            }
            lblFeedback.Visible = false;
            pnlOptions.Enabled = true; // Re-enable panel after feedback timer
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            if (_currentQuestion == null || !_quizManager.IsQuizActive && _quizManager.CurrentQuestionIndex >= _quizManager.Questions.Count-1) return; // Quiz might have just ended

            Button clickedButton = (Button)sender;
            string selectedAnswer = clickedButton.Text;
            bool isCorrect = _quizManager.SubmitAnswer(selectedAnswer);

            lblScore.Text = $"Score: {_quizManager.Score}"; // Update score immediately

            if (_showFeedbackImmediately)
            {
                lblFeedback.Text = isCorrect ? "Correct!" : $"Wrong! The correct answer was: {_currentQuestion.CorrectAnswer}";
                lblFeedback.ForeColor = isCorrect ? Color.Green : Color.Red;
                lblFeedback.Visible = true;

                // Highlight correct/incorrect answers
                foreach (Button btn in _optionButtons)
                {
                    if (btn.Text == _currentQuestion.CorrectAnswer)
                    {
                        btn.BackColor = Color.LightGreen;
                    }
                    else if (btn == clickedButton && !isCorrect) // Clicked wrong answer
                    {
                        btn.BackColor = Color.Salmon;
                    }
                    // btn.Enabled = false; // Disable all options after an answer
                }
                pnlOptions.Enabled = false; // Disable panel containing options
                timerFeedback.Start(); // Wait a bit before loading next question or finishing
            }
            else
            {
                // If no immediate feedback, move to next question or finish
                if (_quizManager.IsQuizActive || _quizManager.CurrentQuestionIndex < _quizManager.Questions.Count -1 ) // Check if there are more questions
                {
                    LoadNextQuestion();
                }
                else
                {
                    QuizFinished();
                }
            }
        }

        private void timerFeedback_Tick(object sender, EventArgs e)
        {
            timerFeedback.Stop();
            lblFeedback.Visible = false;
            // Reset button colors before loading next question
            foreach (Button btn in _optionButtons)
            {
                btn.BackColor = SystemColors.Control;
                // btn.Enabled = true; // Re-enable for next question, handled in UpdateUIForQuestion
            }
            // pnlOptions.Enabled = true; // Re-enable panel, handled in UpdateUIForQuestion
            LoadNextQuestion();
        }

        private void QuizFinished()
        {
            pnlOptions.Enabled = false; // Ensure options are disabled
            lblFeedback.Text = $"Quiz Over! Your final score: {_quizManager.Score} / {_quizManager.TotalQuestionsInQuiz * 10}";
            lblFeedback.ForeColor = SystemColors.ControlText;
            lblFeedback.Visible = true;

            MessageBox.Show($"Quiz Finished!\nYour Score: {_quizManager.Score} out of {_quizManager.TotalQuestionsInQuiz * 10}",
                            "Quiz Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_savePoints && _quizManager.CurrentPlayer != null)
            {
                bool saved = _quizManager.SaveResult();
                if (saved)
                {
                    Console.WriteLine("Score saved successfully via QuizManager.");
                }
                else
                {
                    Console.WriteLine("Failed to save score via QuizManager.");
                     // Optionally inform user via MessageBox if critical
                }
            }
            else if (_savePoints && _quizManager.CurrentPlayer == null) {
                 Console.WriteLine("Warning: Save points was true, but CurrentPlayer was null. Score not saved.");
            }


            this.DialogResult = DialogResult.OK; // Set DialogResult before closing if it's a modal dialog
            this.Close();
        }

    }
}
