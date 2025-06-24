using GeographyQuizAvalonia.Models;
using GeographyQuizAvalonia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // For ObservableCollection
using System.IO; // For Path.Combine
using System.Linq; // For LINQ methods like FirstOrDefault
using System.Reactive; // For ReactiveCommand if using ReactiveUI (simulating basic command for now)
using System.Windows.Input; // For ICommand


namespace GeographyQuizAvalonia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _parentHost; // To navigate
        private readonly PlayerRepository _playerRepository;
        private readonly QuizManagerService _quizManagerService;
        private readonly IniConfigParser _iniParser;

        private string _playerName = string.Empty;
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        public ObservableCollection<string> Continents { get; }
        private string _selectedContinent = "Worldwide";
        public string SelectedContinent
        {
            get => _selectedContinent;
            set => SetProperty(ref _selectedContinent, value);
        }

        public ObservableCollection<QuizTypeEnum> QuizTypes { get; }
        private QuizTypeEnum _selectedQuizType = QuizTypeEnum.CountryFromFlag;
        public QuizTypeEnum SelectedQuizType
        {
            get => _selectedQuizType;
            set => SetProperty(ref _selectedQuizType, value);
        }

        private int _numberOfQuestions = 10;
        public int NumberOfQuestions
        {
            get => _numberOfQuestions;
            set => SetProperty(ref _numberOfQuestions, value, nameof(NumberOfQuestions), nameof(NumberOfQuestionsDisplay));
        }
        public string NumberOfQuestionsDisplay => NumberOfQuestions.ToString();


        private Player? _currentPlayer;

        // Basic ICommand simulation for now. ReactiveUI's ReactiveCommand is more powerful.
        public ICommand StartQuizCommand { get; }
        public ICommand ViewHighScoresCommand { get; }
        public ICommand StartFromConfigCommand { get; }

        private bool _isStartFromConfigEnabled;
        public bool IsStartFromConfigEnabled
        {
            get => _isStartFromConfigEnabled;
            set => SetProperty(ref _isStartFromConfigEnabled, value);
        }


        public MainViewModel(MainWindowViewModel parentHost, PlayerRepository playerRepository, QuizManagerService quizManagerService, IniConfigParser iniParser)
        {
            _parentHost = parentHost;
            _playerRepository = playerRepository;
            _quizManagerService = quizManagerService;
            _iniParser = iniParser;

            Continents = new ObservableCollection<string>
            {
                "Worldwide", "Africa", "Asia", "Europe", "North America", "Oceania", "South America"
            };
            SelectedContinent = Continents.First();

            QuizTypes = new ObservableCollection<QuizTypeEnum>((QuizTypeEnum[])Enum.GetValues(typeof(QuizTypeEnum)));
            SelectedQuizType = QuizTypes.First();

            // Check for ini file for bonus button state
            // In Avalonia, direct Application.StartupPath is not typical. Path should be determined differently,
            // e.g. relative to assembly or passed in. For simulation, assume it's in base directory.
            string baseDirectory = AppContext.BaseDirectory;
            string iniFilePath = Path.Combine(baseDirectory, "quiz.ini");
            IsStartFromConfigEnabled = File.Exists(iniFilePath);


            StartQuizCommand = new RelayCommand(ExecuteStartQuiz, CanExecuteStartQuiz);
            ViewHighScoresCommand = new RelayCommand(ExecuteViewHighScores);
            StartFromConfigCommand = new RelayCommand(ExecuteStartFromConfig, CanExecuteStartFromConfig);
        }

        private bool ValidatePlayerName()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                // TODO: Implement a user notification service instead of Console.WriteLine or MessageBox
                Console.WriteLine("Player name is required.");
                return false;
            }
            _currentPlayer = _playerRepository.GetOrCreatePlayer(PlayerName.Trim());
            if (_currentPlayer == null)
            {
                Console.WriteLine("Failed to get or create player.");
                return false;
            }
            return true;
        }

        private bool CanExecuteStartQuiz(object? parameter) => !string.IsNullOrWhiteSpace(PlayerName);
        private void ExecuteStartQuiz(object? parameter)
        {
            if (!ValidatePlayerName() || _currentPlayer == null) return;

            _quizManagerService.StartQuiz(_currentPlayer, SelectedQuizType, SelectedContinent, NumberOfQuestions, true, true); // Default showFeedback & savePoints for manual start

            if (!_quizManagerService.IsQuizActive || !_quizManagerService.Questions.Any())
            {
                Console.WriteLine("Could not start quiz. Insufficient data.");
                return;
            }
            _parentHost.NavigateToQuiz(new QuizViewModel(_parentHost, _quizManagerService, _currentPlayer));
        }

        private void ExecuteViewHighScores(object? parameter)
        {
            // Ensure current player context is passed if name is entered
             if (!string.IsNullOrWhiteSpace(PlayerName) && _currentPlayer == null)
            {
                _currentPlayer = _playerRepository.GetOrCreatePlayer(PlayerName.Trim());
            }
            _parentHost.NavigateToHighScores(new HighScoresViewModel(_parentHost, _quizManagerService.SavePoints, _playerRepository, _quizManagerService.CurrentPlayer ?? _currentPlayer));
        }

        private bool CanExecuteStartFromConfig(object? parameter) => IsStartFromConfigEnabled && !string.IsNullOrWhiteSpace(PlayerName);
        private void ExecuteStartFromConfig(object? parameter)
        {
            if (!ValidatePlayerName() || _currentPlayer == null) return;

            string baseDirectory = AppContext.BaseDirectory; // Or get path from a service
            string iniFilePath = Path.Combine(baseDirectory, "quiz.ini");

            if (!File.Exists(iniFilePath))
            {
                Console.WriteLine("quiz.ini not found.");
                IsStartFromConfigEnabled = false; // Update state
                // TODO: User notification
                return;
            }

            var config = _iniParser.LoadConfig(iniFilePath);
            if (config == null || !config.Any())
            {
                Console.WriteLine("Failed to load or parse quiz.ini.");
                // TODO: User notification
                return;
            }
            try
            {
                string quizTypeStr = config.GetValueOrDefault("QuizConfig", "fragetyp") ?? "CountryFromFlag";
                string continentStr = config.GetValueOrDefault("QuizConfig", "kontinent") ?? "Worldwide";
                int questions = int.TryParse(config.GetValueOrDefault("QuizConfig", "anzahl_fragen"), out int q) ? q : 10;
                bool showFeedback = (config.GetValueOrDefault("QuizConfig", "feedback_anzeigen") ?? "ja").Equals("ja", StringComparison.OrdinalIgnoreCase);
                bool savePoints = (config.GetValueOrDefault("QuizConfig", "punkte_speichern") ?? "ja").Equals("ja", StringComparison.OrdinalIgnoreCase);

                QuizTypeEnum quizType;
                switch (quizTypeStr.ToLowerInvariant())
                {
                    case "flagge_zu_land": quizType = QuizTypeEnum.CountryFromFlag; break;
                    case "hauptstadt_zu_land": quizType = QuizTypeEnum.CapitalFromCountry; break;
                    case "land_zu_flagge": quizType = QuizTypeEnum.FlagFromCountry; break;
                    case "land_zu_hauptstadt": quizType = QuizTypeEnum.CountryFromCapital; break;
                    case "hauptstadt_zu_flagge": quizType = QuizTypeEnum.CapitalFromFlag; break;
                    case "flagge_zu_hauptstadt": quizType = QuizTypeEnum.FlagFromCapital; break;
                    default:
                        Console.WriteLine($"Invalid 'fragetyp' in quiz.ini: {quizTypeStr}");
                        return;
                }

                _quizManagerService.StartQuiz(_currentPlayer, quizType, continentStr, questions, showFeedback, savePoints);

                if (!_quizManagerService.IsQuizActive || !_quizManagerService.Questions.Any())
                {
                     Console.WriteLine("Could not start quiz from config. Insufficient data.");
                     return;
                }
                _parentHost.NavigateToQuiz(new QuizViewModel(_parentHost, _quizManagerService, _currentPlayer));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error processing quiz.ini: {ex.Message}");
            }
        }
    }

    // Basic RelayCommand implementation for MVVM
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object? parameter) => _execute(parameter);
    }
}
