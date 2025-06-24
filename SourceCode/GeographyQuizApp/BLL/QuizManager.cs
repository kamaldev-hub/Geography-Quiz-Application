using GeographyQuizApp.BLL.Models;
using GeographyQuizApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
// using System.Drawing; // If QuestionImage were Bitmap

namespace GeographyQuizApp.BLL
{
    /// <summary>
    /// Manages the core logic of the quiz, including question generation,
    /// answer checking, and scorekeeping.
    /// </summary>
    public class QuizManager
    {
        private readonly CountryRepository _countryRepository;
        private readonly QuizSessionRepository _quizSessionRepository; // To save results
        private List<Country> _masterCountryList; // Full list of countries for generating incorrect options
        private Random _random;

        /// <summary>
        /// Gets or sets the currently active player.
        /// </summary>
        public Player? CurrentPlayer { get; private set; }

        /// <summary>
        /// Gets or sets the type of the current quiz.
        /// </summary>
        public QuizTypeEnum CurrentQuizType { get; private set; }

        /// <summary>
        /// Gets the list of questions for the current quiz.
        /// </summary>
        public List<Question> Questions { get; private set; }

        /// <summary>
        /// Gets the index of the current question.
        /// </summary>
        public int CurrentQuestionIndex { get; private set; }

        /// <summary>
        /// Gets the current score.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the total number of questions for the current quiz.
        /// </summary>
        public int TotalQuestionsInQuiz { get; private set; }

        /// <summary>
        /// Indicates if the quiz is currently active.
        /// </summary>
        public bool IsQuizActive { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="QuizManager"/> class.
        /// </summary>
        /// <param name="countryRepository">Repository for accessing country data.</param>
        /// <param name="quizSessionRepository">Repository for saving quiz sessions.</param>
        public QuizManager(CountryRepository countryRepository, QuizSessionRepository quizSessionRepository)
        {
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _quizSessionRepository = quizSessionRepository ?? throw new ArgumentNullException(nameof(quizSessionRepository));
            _masterCountryList = new List<Country>();
            Questions = new List<Question>();
            _random = new Random();
            CurrentQuestionIndex = -1;
            Score = 0;
            IsQuizActive = false;
        }

        /// <summary>
        /// Starts a new quiz.
        /// </summary>
        /// <param name="player">The player taking the quiz.</param>
        /// <param name="quizType">The type of quiz to start.</param>
        /// <param name="continent">The continent to filter countries by. Use "Worldwide" or null/empty for all.</param>
        /// <param name="questionCount">The number of questions for the quiz.</param>
        /// <param name="showFeedbackImmediately">Whether to show feedback immediately after each answer.</param>
        /// <param name="savePoints">Whether the points should be saved at the end of the quiz.</param>
        public void StartQuiz(Player player, QuizTypeEnum quizType, string? continent, int questionCount, bool showFeedbackImmediately = true, bool savePoints = true)
        {
            CurrentPlayer = player ?? throw new ArgumentNullException(nameof(player));
            CurrentQuizType = quizType;
            TotalQuestionsInQuiz = Math.Max(1, questionCount); // Ensure at least 1 question
            Score = 0;
            CurrentQuestionIndex = -1;
            Questions.Clear();
            IsQuizActive = true;
            // Future use:
            // _showFeedbackImmediately = showFeedbackImmediately;
            // _savePoints = savePoints;


            // Fetch all countries once to serve as a pool for incorrect answers
            _masterCountryList = _countryRepository.GetAllCountries();
            if (_masterCountryList == null || _masterCountryList.Count < 4) // Need at least 4 for options
            {
                Console.WriteLine("Error: Not enough countries in the database to generate a quiz (need at least 4).");
                IsQuizActive = false;
                return;
            }

            List<Country> quizCountriesPool;
            if (!string.IsNullOrEmpty(continent) && !continent.Equals("Worldwide", StringComparison.OrdinalIgnoreCase))
            {
                quizCountriesPool = _countryRepository.GetCountriesByContinent(continent);
            }
            else
            {
                quizCountriesPool = new List<Country>(_masterCountryList); // Use a copy
            }

            if (quizCountriesPool == null || quizCountriesPool.Count == 0)
            {
                Console.WriteLine($"Error: No countries found for the selected criteria (Continent: {continent}).");
                IsQuizActive = false;
                return;
            }

            // Ensure we don't try to pick more questions than available unique countries
            int actualQuestionCount = Math.Min(TotalQuestionsInQuiz, quizCountriesPool.Count);
            TotalQuestionsInQuiz = actualQuestionCount; // Update if it was reduced

            // Select unique countries for questions
            var selectedCountriesForQuestions = quizCountriesPool.OrderBy(c => _random.Next()).Take(actualQuestionCount).ToList();

            foreach (var countryForQuestion in selectedCountriesForQuestions)
            {
                var question = GenerateQuestion(countryForQuestion, quizType);
                if (question != null)
                {
                    Questions.Add(question);
                }
            }

            if (!Questions.Any())
            {
                Console.WriteLine("Error: Failed to generate any questions for the quiz.");
                IsQuizActive = false;
                return;
            }
        }

        private Question? GenerateQuestion(Country country, QuizTypeEnum quizType)
        {
            var question = new Question();
            List<string> options = new List<string>();
            string correctAnswer;

            switch (quizType)
            {
                case QuizTypeEnum.CountryFromFlag:
                    question.QuestionText = "Which country does this flag belong to?";
                    question.ImagePath = country.FlagImagePath; // UI will load this
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
                    question.QuestionText = $"Which flag represents {country.Name}?";
                    // The "answer" is identifying the correct flag. Visually, this means picking the country name associated with the displayed flag.
                    // For button options, we show country names. The UI will need to display one flag (country.FlagImagePath) as the question.
                    // This is tricky. The question is "Which flag represents {Country Name}?"
                    // UI should show country.FlagImagePath. Options are other country names.
                    // Or, Question: "Which flag is this?" Image: country.FlagImagePath. Options: Country Names. This is CountryFromFlag.
                    // Let's assume: Question Text shows country name. Options are images of flags (represented by their paths or country names).
                    // For simplicity with button text, we'll make the options country names, and the UI implies "select the country whose flag matches the question"
                    // This is more like "Identify the country whose flag is shown implicitly with its name"
                    // To make it work with text options: Question: "Identify the flag of {country.Name}"
                    // Correct Answer: country.Name (representing its flag). Options: Other country names.
                    // This means the user has to *know* the flag of country.Name and select its name from options.
                    // The AGENTS.md implies the answer is a string.
                    // If options are flag images, then answer is country.Name.
                    // If options are country names, then question implies "Which of these is {country.Name}?" (and its flag is implicitly shown).
                    // Let's stick to: Question: "What is the flag of {country.Name}?" Answer: country.FlagImagePath (or country.Name as proxy)
                    // Options: Paths to other flags (or country names as proxies).
                    question.QuestionText = $"What is the flag of {country.Name}?";
                    correctAnswer = country.Name; // User selects the name of the country whose flag is implicitly being asked for.
                                                  // UI could show multiple flags if capabilities allow, but buttons are text.
                    question.ImagePath = country.FlagImagePath; // This is the target flag the question is about.
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3, c => c.FlagImagePath != country.FlagImagePath));
                    break;


                case QuizTypeEnum.CountryFromCapital:
                    question.QuestionText = $"Which country has {country.Capital} as its capital?";
                    correctAnswer = country.Name;
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3, c => c.Capital != country.Capital));
                    break;

                case QuizTypeEnum.CapitalFromFlag:
                    question.QuestionText = "What is the capital of the country represented by this flag?";
                    question.ImagePath = country.FlagImagePath;
                    correctAnswer = country.Capital;
                    options.Add(country.Capital);
                    options.AddRange(GetRandomCapitals(country.Capital, 3, country.Name, c => c.FlagImagePath != country.FlagImagePath));
                    break;

                case QuizTypeEnum.FlagFromCapital:
                    question.QuestionText = $"What is the flag of the country whose capital is {country.Capital}?";
                    question.ImagePath = country.FlagImagePath; // The flag of the country whose capital is country.Capital
                    correctAnswer = country.Name; // User selects the name of the country.
                    options.Add(country.Name);
                    options.AddRange(GetRandomCountryNames(country.Name, 3, c => c.Capital != country.Capital && c.FlagImagePath != country.FlagImagePath));
                    break;

                default:
                    Console.WriteLine($"Unsupported quiz type: {quizType}");
                    return null;
            }

            question.CorrectAnswer = correctAnswer;
            question.Options = options.OrderBy(o => _random.Next()).ToList(); // Shuffle options
            return question;
        }

        private List<string> GetRandomCountryNames(string excludeName, int count, Func<Country, bool>? additionalFilter = null)
        {
            var filteredCountries = _masterCountryList.Where(c => c.Name != excludeName);
            if (additionalFilter != null)
            {
                filteredCountries = filteredCountries.Where(additionalFilter);
            }
            return filteredCountries
                .OrderBy(c => _random.Next())
                .Take(count)
                .Select(c => c.Name)
                .ToList();
        }

        private List<string> GetRandomCapitals(string excludeCapital, int count, string excludeCountryNameForCapitalSource, Func<Country, bool>? additionalFilter = null)
        {
             // Ensure we don't pick the capital of the same country if the question is about linking capital to a different aspect of the *same* country
            var filteredCountries = _masterCountryList.Where(c => c.Capital != excludeCapital && c.Name != excludeCountryNameForCapitalSource);
            if (additionalFilter != null)
            {
                filteredCountries = filteredCountries.Where(additionalFilter);
            }
            return filteredCountries
                .Select(c => c.Capital)
                .Distinct() // Capitals might not be unique across all (though rare for major ones)
                .OrderBy(cap => _random.Next())
                .Take(count)
                .ToList();
        }


        /// <summary>
        /// Retrieves the next question in the quiz.
        /// </summary>
        /// <returns>The next Question object, or null if the quiz is over or not started.</returns>
        public Question? GetNextQuestion()
        {
            if (!IsQuizActive || Questions == null || !Questions.Any()) return null;

            CurrentQuestionIndex++;
            if (CurrentQuestionIndex < Questions.Count)
            {
                return Questions[CurrentQuestionIndex];
            }
            else
            {
                IsQuizActive = false; // Quiz is over
                return null;
            }
        }

        /// <summary>
        /// Submits an answer for the current question and updates the score.
        /// </summary>
        /// <param name="selectedAnswer">The answer selected by the user.</param>
        /// <returns>True if the answer was correct, false otherwise. Returns false if no quiz is active or no question is loaded.</returns>
        public bool SubmitAnswer(string selectedAnswer)
        {
            if (!IsQuizActive || CurrentQuestionIndex < 0 || CurrentQuestionIndex >= Questions.Count)
            {
                return false;
            }

            var currentQuestion = Questions[CurrentQuestionIndex];
            bool isCorrect = currentQuestion.CorrectAnswer.Equals(selectedAnswer, StringComparison.OrdinalIgnoreCase);

            if (isCorrect)
            {
                Score += 10; // Example scoring: 10 points per correct answer
            }

            // If this is the last question, mark quiz as inactive for next GetNextQuestion call
            if (CurrentQuestionIndex == Questions.Count - 1)
            {
                IsQuizActive = false;
            }

            return isCorrect;
        }

        /// <summary>
        /// Saves the result of the current quiz for the player.
        /// Should be called after the quiz is completed.
        /// </summary>
        /// <returns>True if the result was saved successfully, false otherwise.</returns>
        public bool SaveResult()
        {
            if (CurrentPlayer == null || Questions == null || !Questions.Any())
            {
                Console.WriteLine("BLL.QuizManager.SaveResult(): Cannot save result - quiz not properly initialized or completed.");
                return false;
            }
             if (IsQuizActive && CurrentQuestionIndex < Questions.Count -1) {
                Console.WriteLine("BLL.QuizManager.SaveResult(): Cannot save result - quiz is still active and not all questions answered.");
                // Or force end quiz? For now, let's assume it's called when quiz naturally ends.
                // IsQuizActive should be false if GetNextQuestion returned null or SubmitAnswer was for the last question.
            }


            var session = new QuizSession
            {
                PlayerID = CurrentPlayer.PlayerID, // Essential
                Player = CurrentPlayer, // Good to have for context, DAL might only use PlayerID
                Score = this.Score,
                QuizTimestamp = DateTime.UtcNow,
                TotalQuestions = this.TotalQuestionsInQuiz, // Use the actual number of questions presented
                QuizType = this.CurrentQuizType.ToString()
            };

            bool success = _quizSessionRepository.SaveSession(session);
            if(success)
            {
                Console.WriteLine($"Quiz result saved for player {CurrentPlayer.PlayerName}, Score: {this.Score}");
            } else {
                Console.WriteLine($"Failed to save quiz result for player {CurrentPlayer.PlayerName}");
            }
            return success;
        }
    }
}
