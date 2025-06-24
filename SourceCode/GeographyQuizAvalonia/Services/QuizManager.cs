using GeographyQuizAvalonia.Models;   // Updated namespace
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeographyQuizAvalonia.Services // Changed namespace
{
    /// <summary>
    /// Manages the core logic of the quiz.
    /// </summary>
    public class QuizManagerService // Renamed to avoid conflict if a QuizManager ViewModel is created
    {
        private readonly CountryRepository _countryRepository;
        private readonly QuizSessionRepository _quizSessionRepository;
        private List<Country> _masterCountryList;
        private Random _random;

        public Player? CurrentPlayer { get; private set; }
        public QuizTypeEnum CurrentQuizType { get; private set; }
        public List<Question> Questions { get; private set; }
        public int CurrentQuestionIndex { get; private set; }
        public int Score { get; private set; }
        public int TotalQuestionsInQuiz { get; private set; }
        public bool IsQuizActive { get; private set; }

        // Configuration from .ini, to be passed during StartQuiz by ViewModel
        public bool ShowFeedbackImmediately { get; private set; } = true;
        public bool SavePoints { get; private set; } = true;


        public QuizManagerService(CountryRepository countryRepository, QuizSessionRepository quizSessionRepository)
        {
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _quizSessionRepository = quizSessionRepository ?? throw new ArgumentNullException(nameof(quizSessionRepository));
            _masterCountryList = new List<Country>();
            Questions = new List<Question>();
            _random = new Random();
            CurrentQuestionIndex = -1;
            IsQuizActive = false;
        }

        public void StartQuiz(Player player, QuizTypeEnum quizType, string? continent, int questionCount, bool showFeedback, bool savePoints)
        {
            CurrentPlayer = player ?? throw new ArgumentNullException(nameof(player));
            CurrentQuizType = quizType;
            TotalQuestionsInQuiz = Math.Max(1, questionCount);
            Score = 0;
            CurrentQuestionIndex = -1;
            Questions.Clear();
            IsQuizActive = true;
            ShowFeedbackImmediately = showFeedback; // Store config
            SavePoints = savePoints;               // Store config

            _masterCountryList = _countryRepository.GetAllCountries();
            if (_masterCountryList == null || _masterCountryList.Count < 4)
            {
                Console.WriteLine("Error: Not enough countries (need at least 4).");
                IsQuizActive = false;
                // In a real app, this should propagate an error/status to the ViewModel
                return;
            }

            List<Country> quizCountriesPool = string.IsNullOrEmpty(continent) || continent.Equals("Worldwide", StringComparison.OrdinalIgnoreCase)
                ? new List<Country>(_masterCountryList)
                : _countryRepository.GetCountriesByContinent(continent);

            if (quizCountriesPool == null || !quizCountriesPool.Any())
            {
                Console.WriteLine($"Error: No countries found for criteria (Continent: {continent}).");
                IsQuizActive = false;
                return;
            }

            int actualQuestionCount = Math.Min(TotalQuestionsInQuiz, quizCountriesPool.Count);
            TotalQuestionsInQuiz = actualQuestionCount;

            var selectedCountriesForQuestions = quizCountriesPool.OrderBy(c => _random.Next()).Take(actualQuestionCount).ToList();

            foreach (var countryForQuestion in selectedCountriesForQuestions)
            {
                var question = GenerateQuestion(countryForQuestion, quizType);
                if (question != null) Questions.Add(question);
            }

            if (!Questions.Any())
            {
                Console.WriteLine("Error: Failed to generate any questions.");
                IsQuizActive = false;
                return;
            }
        }

        private Question? GenerateQuestion(Country country, QuizTypeEnum quizType)
        {
            var question = new Question();
            var options = new List<string>();
            string correctAnswer;
            // Ensure FlagImagePath uses Avalonia URI scheme if not already
            // The CountryRepository simulation already sets this to "avares://..."
            string avaloniaFlagPath = country.FlagImagePath;

            switch (quizType)
            {
                case QuizTypeEnum.CountryFromFlag:
                    question.QuestionText = "Which country does this flag belong to?";
                    question.ImagePath = avaloniaFlagPath;
                    correctAnswer = country.Name;
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3));
                    break;

                case QuizTypeEnum.CapitalFromCountry:
                    question.QuestionText = $"What is the capital of {country.Name}?";
                    correctAnswer = country.Capital;
                    options.Add(country.Capital);
                    options.AddRange(GetRandomCapitals(country.Capital, 3, country.Name));
                    break;

                case QuizTypeEnum.FlagFromCountry:
                    question.QuestionText = $"What is the flag of {country.Name}?";
                    question.ImagePath = avaloniaFlagPath; // The question implies this flag.
                    correctAnswer = country.Name; // Answer by selecting the country name.
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3, c => c.FlagImagePath != avaloniaFlagPath));
                    break;

                case QuizTypeEnum.CountryFromCapital:
                    question.QuestionText = $"Which country has {country.Capital} as its capital?";
                    correctAnswer = country.Name;
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3, c => c.Capital != country.Capital));
                    break;

                case QuizTypeEnum.CapitalFromFlag:
                    question.QuestionText = "What is the capital of the country represented by this flag?";
                    question.ImagePath = avaloniaFlagPath;
                    correctAnswer = country.Capital;
                    options.Add(country.Capital);
                    options.AddRange(GetRandomCapitals(country.Capital, 3, country.Name, c => c.FlagImagePath != avaloniaFlagPath));
                    break;

                case QuizTypeEnum.FlagFromCapital:
                    question.QuestionText = $"What is the flag of the country whose capital is {country.Capital}?";
                    question.ImagePath = avaloniaFlagPath;
                    correctAnswer = country.Name;
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3, c => c.Capital != country.Capital && c.FlagImagePath != avaloniaFlagPath));
                    break;

                default:
                    Console.WriteLine($"Unsupported quiz type: {quizType}");
                    return null;
            }

            question.CorrectAnswer = correctAnswer;
            question.Options = options.OrderBy(o => _random.Next()).ToList();
            return question;
        }

        private List<string> GetRandomCountryNames(string excludeName, int count, Func<Country, bool>? additionalFilter = null)
        {
            var filtered = _masterCountryList.Where(c => c.Name != excludeName);
            if (additionalFilter != null) filtered = filtered.Where(additionalFilter);
            return filtered.OrderBy(c => _random.Next()).Take(count).Select(c => c.Name).ToList();
        }

        private List<string> GetRandomCapitals(string excludeCapital, int count, string excludeCountryName, Func<Country, bool>? additionalFilter = null)
        {
            var filtered = _masterCountryList.Where(c => c.Capital != excludeCapital && c.Name != excludeCountryName);
            if (additionalFilter != null) filtered = filtered.Where(additionalFilter);
            return filtered.Select(c => c.Capital).Distinct().OrderBy(cap => _random.Next()).Take(count).ToList();
        }

        public Question? GetNextQuestion()
        {
            if (!IsQuizActive || Questions == null || !Questions.Any()) return null;
            CurrentQuestionIndex++;
            if (CurrentQuestionIndex < Questions.Count) return Questions[CurrentQuestionIndex];

            IsQuizActive = false; // Quiz is over
            return null;
        }

        public bool SubmitAnswer(string selectedAnswer)
        {
            if (CurrentQuestionIndex < 0 || CurrentQuestionIndex >= Questions.Count) return false;

            var currentQuestion = Questions[CurrentQuestionIndex];
            bool isCorrect = currentQuestion.CorrectAnswer.Equals(selectedAnswer, StringComparison.OrdinalIgnoreCase);

            if (isCorrect) Score += 10;

            if (CurrentQuestionIndex == Questions.Count - 1) IsQuizActive = false;
            return isCorrect;
        }

        public bool SaveResult()
        {
            if (CurrentPlayer == null || Questions == null || !Questions.Any())
            {
                Console.WriteLine("Services.QuizManagerService.SaveResult(): Cannot save - quiz not properly initialized/completed.");
                return false;
            }
            if (!SavePoints) // Check the flag from .ini/config
            {
                Console.WriteLine("Services.QuizManagerService.SaveResult(): Points saving is disabled by configuration.");
                return false;
            }

            var session = new QuizSession
            {
                PlayerID = CurrentPlayer.PlayerID,
                Player = CurrentPlayer,
                Score = this.Score,
                QuizTimestamp = DateTime.UtcNow,
                TotalQuestions = this.TotalQuestionsInQuiz,
                QuizType = this.CurrentQuizType.ToString()
            };

            bool success = _quizSessionRepository.SaveSession(session);
            if(success) Console.WriteLine($"Quiz result saved for player {CurrentPlayer.PlayerName}, Score: {this.Score}");
            else Console.WriteLine($"Failed to save quiz result for player {CurrentPlayer.PlayerName}");
            return success;
        }
    }
}
