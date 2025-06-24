using System.Collections.Generic;

namespace GeographyQuizAvalonia.Models
{
    /// <summary>
    /// Represents a single question in the quiz.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Gets or sets the text of the question (e.g., "What is the capital of...?").
        /// </summary>
        public string QuestionText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the correct answer to the question.
        /// </summary>
        public string CorrectAnswer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of answer options, including the correct answer.
        /// </summary>
        public List<string> Options { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the path to the image file (e.g., a flag).
        /// For Avalonia, this path will be an asset path like "avares://AppName/Assets/Flags/flag.png"
        /// or a path relative to the executable that the UI layer will resolve.
        /// </summary>
        public string? ImagePath { get; set; }
    }
}
