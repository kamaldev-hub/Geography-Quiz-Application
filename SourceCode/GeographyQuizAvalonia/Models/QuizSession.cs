using System;

namespace GeographyQuizAvalonia.Models
{
    /// <summary>
    /// Represents a single quiz session played by a user, including their score.
    /// </summary>
    public class QuizSession
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quiz session.
        /// </summary>
        public int SessionID { get; set; }

        /// <summary>
        /// Gets or sets the player who played this quiz session.
        /// In MVVM, this might be populated by the ViewModel or a service.
        /// </summary>
        public Player? Player { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the Player.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the score achieved in this quiz session.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the quiz session was completed.
        /// </summary>
        public DateTime QuizTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the total number of questions in this quiz session.
        /// </summary>
        public int TotalQuestions { get; set; }

        /// <summary>
        /// Gets or sets the type of quiz played (e.g., "CountryFromFlag").
        /// </summary>
        public string QuizType { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizSession"/> class.
        /// </summary>
        public QuizSession()
        {
            QuizTimestamp = DateTime.UtcNow;
        }
    }
}
