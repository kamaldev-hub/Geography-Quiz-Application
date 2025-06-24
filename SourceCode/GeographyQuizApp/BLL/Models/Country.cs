using System;
using System.Collections.Generic;

namespace GeographyQuizApp.BLL.Models
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the capital city of the country.
        /// </summary>
        public string Capital { get; set; }

        /// <summary>
        /// Gets or sets the continent where the country is located.
        /// </summary>
        public string Continent { get; set; } // Representing ENUM('Asia', 'Europe', 'Africa', 'North America', 'South America', 'Oceania')

        /// <summary>
        /// Gets or sets the relative path to the country's flag image.
        /// </summary>
        public string FlagImagePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        public Country()
        {
            Name = string.Empty;
            Capital = string.Empty;
            Continent = string.Empty;
            FlagImagePath = string.Empty;
        }
    }
}
