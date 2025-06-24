using GeographyQuizAvalonia.Models;
using GeographyQuizAvalonia.Services;
using System;
using System.Collections.ObjectModel; // For ObservableCollection
using System.Linq;
using System.Reactive; // For ReactiveCommand or System.Windows.Input for ICommand
using System.Windows.Input; // For ICommand
using System.Threading.Tasks; // For Task.Delay

namespace GeographyQuizAvalonia.ViewModels
{
    public class QuizViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _parentHost;
        private readonly QuizManagerService _quizManager;
        private readonly Player? _currentPlayer; // Keep track of who is playing

        private Question? _currentQuestion;
        public Question? CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        private string _questionNumberText = string.Empty;
        public string QuestionNumberText
        {
            get => _questionNumberText;
            set => SetProperty(ref _questionNumberText, value);
        }

        private string _scoreText = string.Empty;
        public string ScoreText
        {
            get => _scoreText;
            set => SetProperty(ref _scoreText, value);
        }

        private string _feedbackText = string.Empty;
        public string FeedbackText
        {
            get => _feedbackText;
            set => SetProperty(ref _feedbackText, value);
        }

        private bool _isFeedbackVisible;
        public bool IsFeedbackVisible
        {
            get => _isFeedbackVisible;
            set => SetProperty(ref _isFeedbackVisible, value);
        }

        // In Avalonia, colors are often handled by styles and brushes, not direct Color objects in VM
        // We can use string properties to indicate state e.g. "Correct", "Incorrect", "Neutral"
        private string _feedbackState = "Neutral"; // "Correct", "Incorrect"
        public string FeedbackState
        {
            get => _feedbackState;
            set => SetProperty(ref _feedbackState, value);
        }


        public ObservableCollection<string> AnswerOptions { get; } = new ObservableCollection<string>();

        public ICommand SubmitAnswerCommand { get; }

        private bool _areOptionsEnabled = true;
        public bool AreOptionsEnabled
        {
            get => _areOptionsEnabled;
            set => SetProperty(ref _areOptionsEnabled, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private int _progressMax;
        public int ProgressMax
        {
            get => _progressMax;
            set => SetProperty(ref _progressMax, value);
        }


        public QuizViewModel(MainWindowViewModel parentHost, QuizManagerService quizManager, Player? player)
        {
            _parentHost = parentHost;
            _quizManager = quizManager;
            _currentPlayer = player; // May or may not be used directly here, but good for context

            SubmitAnswerCommand = new RelayCommand(ExecuteSubmitAnswer, CanExecuteSubmitAnswer);

            ProgressMax = _quizManager.TotalQuestionsInQuiz;
            LoadNextQuestion();
        }

        private void LoadNextQuestion()
        {
            AreOptionsEnabled = true;
            IsFeedbackVisible = false;
            FeedbackState = "Neutral";
            CurrentQuestion = _quizManager.GetNextQuestion();

            if (CurrentQuestion != null)
            {
                QuestionNumberText = $"Question: {_quizManager.CurrentQuestionIndex + 1} / {_quizManager.TotalQuestionsInQuiz}";
                ScoreText = $"Score: {_quizManager.Score}";
                ProgressValue = _quizManager.CurrentQuestionIndex +1;

                AnswerOptions.Clear();
                foreach (var option in CurrentQuestion.Options)
                {
                    AnswerOptions.Add(option);
                }
            }
            else
            {
                QuizFinished();
            }
        }

        private bool CanExecuteSubmitAnswer(object? parameter) => AreOptionsEnabled && CurrentQuestion != null;

        private async void ExecuteSubmitAnswer(object? parameter)
        {
            if (parameter is string selectedAnswer && CurrentQuestion != null)
            {
                AreOptionsEnabled = false;
                bool isCorrect = _quizManager.SubmitAnswer(selectedAnswer);
                ScoreText = $"Score: {_quizManager.Score}"; // Update score

                if (_quizManager.ShowFeedbackImmediately)
                {
                    FeedbackText = isCorrect ? "Correct!" : $"Wrong! The correct answer was: {CurrentQuestion.CorrectAnswer}";
                    FeedbackState = isCorrect ? "Correct" : "Incorrect";
                    IsFeedbackVisible = true;

                    await Task.Delay(1500); // Simulate feedback display time

                    if (!_quizManager.IsQuizActive && _quizManager.CurrentQuestionIndex >= _quizManager.Questions.Count -1)
                    {
                         QuizFinished(); // Call QuizFinished if it was the last question after feedback
                    }
                    else
                    {
                        LoadNextQuestion();
                    }
                }
                else // No immediate feedback
                {
                     if (!_quizManager.IsQuizActive && _quizManager.CurrentQuestionIndex >= _quizManager.Questions.Count -1)
                    {
                         QuizFinished();
                    }
                    else
                    {
                        LoadNextQuestion();
                    }
                }
            }
        }

        private void QuizFinished()
        {
            AreOptionsEnabled = false;
            FeedbackText = $"Quiz Over! Your final score: {_quizManager.Score} / {_quizManager.TotalQuestionsInQuiz * 10}";
            FeedbackState = "Neutral";
            IsFeedbackVisible = true;
            QuestionNumberText = "Quiz Finished!";

            // TODO: Implement a dialog service for showing messages in Avalonia
            Console.WriteLine($"Quiz Finished! Score: {_quizManager.Score}");

            if (_quizManager.SavePoints) // Check config from QuizManager
            {
                _quizManager.SaveResult();
            }

            // Consider a delay or a "Return to Main Menu" button instead of auto-navigating
            // For now, let's navigate back after a short delay
            Task.Run(async () => {
                await Task.Delay(2500); // Give user time to see final score message
                // Dispatch to UI thread if needed for navigation, Avalonia handles this for DataContext changes often
                _parentHost.NavigateToMain();
            });
        }
    }
}
