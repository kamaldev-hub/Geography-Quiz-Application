using GeographyQuizAvalonia.Models;
using GeographyQuizAvalonia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GeographyQuizAvalonia.ViewModels
{
    // Helper class for displaying scores in the view, can be nested or separate
    public class DisplayHighScoreViewModel : ViewModelBase // Inherit for potential future bindings
    {
        public string PlayerName { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime QuizTimestamp { get; set; }
        public string QuizType { get; set; } = string.Empty;
        public int TotalQuestions { get; set; }
    }

    public class HighScoresViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _parentHost;
        private readonly bool _canSavePoints; // From QuizManager config, though not directly used here other than for context
        private readonly PlayerRepository _playerRepository;
        private readonly QuizSessionRepository _quizSessionRepository;
        private readonly Player? _currentPlayerContext; // Player context from MainViewModel

        public ObservableCollection<DisplayHighScoreViewModel> HighScores { get; } = new ObservableCollection<DisplayHighScoreViewModel>();

        private bool _showAllPlayers = true;
        public bool ShowAllPlayers
        {
            get => _showAllPlayers;
            set
            {
                if (SetProperty(ref _showAllPlayers, value))
                {
                    if (value) _showMyScores = false; // Ensure radio button behavior
                    OnPropertyChanged(nameof(ShowMyScores));
                    LoadHighScores();
                }
            }
        }

        private bool _showMyScores;
        public bool ShowMyScores
        {
            get => _showMyScores;
            set
            {
                if (SetProperty(ref _showMyScores, value))
                {
                    if (value) _showAllPlayers = false; // Ensure radio button behavior
                    OnPropertyChanged(nameof(ShowAllPlayers));
                    LoadHighScores();
                }
            }
        }

        public bool CanShowMyScores { get; }


        public ICommand CloseCommand { get; }

        public HighScoresViewModel(MainWindowViewModel parentHost, bool canSavePoints, PlayerRepository playerRepository, Player? currentPlayerContext)
        {
            _parentHost = parentHost;
            _canSavePoints = canSavePoints; // Store if needed for any logic
            _playerRepository = playerRepository;
            _quizSessionRepository = new QuizSessionRepository(new DatabaseConnector()); // Usually injected
            _currentPlayerContext = currentPlayerContext;

            CanShowMyScores = _currentPlayerContext != null;
            if (!CanShowMyScores) ShowAllPlayers = true; // Default if "My Scores" is not applicable
            else ShowMyScores = true; // Default to player scores if context available

            CloseCommand = new RelayCommand(_ => _parentHost.NavigateToMain());
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            HighScores.Clear();
            List<QuizSession> scores;

            if (ShowMyScores && _currentPlayerContext != null)
            {
                scores = _quizSessionRepository.GetScoresByPlayer(_currentPlayerContext.PlayerID);
            }
            else
            {
                scores = _quizSessionRepository.GetAllScores();
            }

            if (scores == null) return;

            // Fetch all player details once for name lookup (simulated efficiency)
            var allPlayers = _playerRepository.GetAllPlayers();

            foreach (var session in scores.OrderByDescending(s => s.Score).ThenByDescending(s => s.QuizTimestamp))
            {
                Player? player = session.Player ?? allPlayers.FirstOrDefault(p => p.PlayerID == session.PlayerID);
                HighScores.Add(new DisplayHighScoreViewModel
                {
                    PlayerName = player?.PlayerName ?? "Unknown",
                    Score = session.Score,
                    QuizTimestamp = session.QuizTimestamp,
                    QuizType = session.QuizType,
                    TotalQuestions = session.TotalQuestions
                });
            }
        }
    }
}
