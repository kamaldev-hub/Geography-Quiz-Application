using System; // Retain for DateTime if used, though not in this specific model

namespace GeographyQuizAvalonia.Models
{
    /// <summary>
    /// Represents a country with its associated geographical data.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the unique identifier for the country.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the capital city of the country.
        /// </summary>
        public string Capital { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the continent where the country is located.
        /// </summary>
        public string Continent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the relative path to the country's flag image.
        /// For Avalonia, this path will likely be relative to an Assets folder.
        /// Example: "avares://GeographyQuizAvalonia/Assets/Flags/DE.png" or a relative path from executable.
        /// </summary>
        public string FlagImagePath { get; set; } = string.Empty;
    }
}
