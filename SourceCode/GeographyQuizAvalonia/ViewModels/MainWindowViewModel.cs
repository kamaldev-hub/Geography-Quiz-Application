using GeographyQuizAvalonia.Services; // For potential service injections
using System;

namespace GeographyQuizAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly MainViewModel _mainViewModel; // Keep instance to return to it

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        // Services would be injected here via constructor in a DI setup
        // For now, ViewModels might instantiate services or they are passed down
        public MainWindowViewModel()
        {
            // Initialize services needed by child ViewModels if not using DI
            // For simplicity, let's assume MainViewModel will create its own required services for now,
            // or they are passed to it.
            // In a real app, these would likely be singletons or scoped services.
            var dbConnector = new DatabaseConnector();
            var countryRepo = new CountryRepository(dbConnector);
            var playerRepo = new PlayerRepository(dbConnector);
            var quizSessionRepo = new QuizSessionRepository(dbConnector);
            var quizManagerService = new QuizManagerService(countryRepo, quizSessionRepo);
            var iniParser = new IniConfigParser();

            _mainViewModel = new MainViewModel(this, playerRepo, quizManagerService, iniParser); // Pass dependencies
            _currentView = _mainViewModel;
        }

        public void NavigateToQuiz(QuizViewModel quizViewModel)
        {
            CurrentView = quizViewModel;
        }

        public void NavigateToHighScores(HighScoresViewModel highScoresViewModel)
        {
            CurrentView = highScoresViewModel;
        }

        public void NavigateToMain()
        {
            CurrentView = _mainViewModel; // Return to the existing MainViewModel instance
        }
    }
}
