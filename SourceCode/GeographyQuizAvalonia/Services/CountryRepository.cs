using GeographyQuizAvalonia.Models; // Updated namespace for Model
using System;
using System.Collections.Generic;
using System.Linq; // Added for Where/Equals simulation

// using MySql.Data.MySqlClient; // Actual MySQL connector (not used in simulation)

namespace GeographyQuizAvalonia.Services // Changed namespace
{
    /// <summary>
    /// Provides data access functionalities for Country entities (simulated).
    /// </summary>
    public class CountryRepository
    {
        private readonly DatabaseConnector _dbConnector;
        private readonly List<Country> _simulatedCountries; // Hold simulated data

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="dbConnector">The database connector to use for database operations.</param>
        public CountryRepository(DatabaseConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
            // Initialize with placeholder data for simulation
            _simulatedCountries = new List<Country>
            {
                new Country { CountryID = 1, Name = "Germany", Capital = "Berlin", Continent = "Europe", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/DE.png" },
                new Country { CountryID = 2, Name = "France", Capital = "Paris", Continent = "Europe", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/FR.png" },
                new Country { CountryID = 3, Name = "Japan", Capital = "Tokyo", Continent = "Asia", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/JP.png" },
                new Country { CountryID = 4, Name = "Canada", Capital = "Ottawa", Continent = "North America", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/CA.png" }, // Placeholder, CA.png not added yet
                new Country { CountryID = 5, Name = "Brazil", Capital = "Brasilia", Continent = "South America", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/BR.png" }, // Placeholder
                new Country { CountryID = 6, Name = "Australia", Capital = "Canberra", Continent = "Oceania", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/AU.png" }, // Placeholder
                new Country { CountryID = 7, Name = "South Africa", Capital = "Pretoria", Continent = "Africa", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/ZA.png" } // Placeholder
            };
            // Add US as one of the placeholder flags I created
             _simulatedCountries.Add(new Country { CountryID = 8, Name = "United States", Capital = "Washington D.C.", Continent = "North America", FlagImagePath = "avares://GeographyQuizAvalonia/Assets/Flags/US.png" });

        }

        /// <summary>
        /// Retrieves all countries from the database (simulated).
        /// </summary>
        /// <returns>A list of all countries.</returns>
        public List<Country> GetAllCountries()
        {
            Console.WriteLine("Services.CountryRepository.GetAllCountries(): Simulating data retrieval.");
            return new List<Country>(_simulatedCountries); // Return a copy
        }

        /// <summary>
        /// Retrieves all countries belonging to a specific continent (simulated).
        /// </summary>
        /// <param name="continent">The continent to filter by.</param>
        /// <returns>A list of countries from the specified continent.</returns>
        public List<Country> GetCountriesByContinent(string continent)
        {
            Console.WriteLine($"Services.CountryRepository.GetCountriesByContinent({continent}): Simulating data retrieval.");

            if (string.IsNullOrWhiteSpace(continent) || continent.Equals("Worldwide", StringComparison.OrdinalIgnoreCase))
            {
                return GetAllCountries();
            }

            return _simulatedCountries.Where(c => c.Continent.Equals(continent, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
