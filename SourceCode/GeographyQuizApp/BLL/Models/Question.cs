using System;
using System.Collections.Generic;
// using System.Drawing; // For Bitmap, if used. For simulation, object is fine.

namespace GeographyQuizApp.BLL.Models
{
    /// <summary>
    /// Represents a single question in the quiz.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Gets or sets the text of the question (e.g., "What is the capital of...?").
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the correct answer to the question.
        /// </summary>
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// Gets or sets the list of answer options, including the correct answer.
        /// </summary>
        public List<string> Options { get; set; }

        /// <summary>
        /// Gets or sets the image associated with the question (e.g., a flag).
        /// This could be a Bitmap object in a real application, or a path to an image.
        /// For simulation, using object or string for path.
        /// </summary>
        public object? QuestionImage { get; set; } // Could be string (path) or System.Drawing.Image/Bitmap

        /// <summary>
        /// Gets or sets the path to the image file, if applicable.
        /// This is more aligned with the Country.FlagImagePath.
        /// </summary>
        public string? ImagePath { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Question"/> class.
        /// </summary>
        public Question()
        {
            QuestionText = string.Empty;
            CorrectAnswer = string.Empty;
            Options = new List<string>();
        }
    }
}
