using System;

namespace GeographyQuizApp.BLL.Models
{
    /// <summary>
    /// Represents a player of the quiz.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the unique identifier for the player.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the player was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            PlayerName = string.Empty;
            CreatedAt = DateTime.UtcNow; // Default to current time
        }
    }
}
