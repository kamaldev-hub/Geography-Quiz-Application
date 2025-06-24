using GeographyQuizApp.BLL.Models;
using System;
using System.Collections.Generic;
// using MySql.Data.MySqlClient; // Actual MySQL connector

namespace GeographyQuizApp.DAL
{
    /// <summary>
    /// Provides data access functionalities for Country entities.
    /// </summary>
    public class CountryRepository
    {
        private readonly DatabaseConnector _dbConnector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="dbConnector">The database connector to use for database operations.</param>
        public CountryRepository(DatabaseConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
        }

        /// <summary>
        /// Retrieves all countries from the database.
        /// </summary>
        /// <returns>A list of all countries. Returns an empty list if an error occurs or no countries are found.</returns>
        public List<Country> GetAllCountries()
        {
            var countries = new List<Country>();
            // Simulated database call
            Console.WriteLine("DAL.CountryRepository.GetAllCountries(): Simulating database call.");

            // Example of how it would work with actual MySql
            /*
            try
            {
                using (var connection = _dbConnector.GetActualConnection()) // Assuming GetActualConnection returns MySqlConnection
                {
                    connection.Open();
                    string query = "SELECT CountryID, Name, Capital, Continent, FlagImagePath FROM Countries;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countries.Add(new Country
                                {
                                    CountryID = reader.GetInt32("CountryID"),
                                    Name = reader.GetString("Name"),
                                    Capital = reader.GetString("Capital"),
                                    Continent = reader.GetString("Continent"),
                                    FlagImagePath = reader.GetString("FlagImagePath")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error fetching all countries: {ex.Message}");
                // Log error, handle exception appropriately
            }
            */

            // Placeholder data for simulation
            countries.Add(new Country { CountryID = 1, Name = "Germany", Capital = "Berlin", Continent = "Europe", FlagImagePath = "Flags/DE.png" });
            countries.Add(new Country { CountryID = 2, Name = "France", Capital = "Paris", Continent = "Europe", FlagImagePath = "Flags/FR.png" });
            countries.Add(new Country { CountryID = 3, Name = "Japan", Capital = "Tokyo", Continent = "Asia", FlagImagePath = "Flags/JP.png" });
            countries.Add(new Country { CountryID = 4, Name = "Canada", Capital = "Ottawa", Continent = "North America", FlagImagePath = "Flags/CA.png" });
            countries.Add(new Country { CountryID = 5, Name = "Brazil", Capital = "Brasilia", Continent = "South America", FlagImagePath = "Flags/BR.png" });
            countries.Add(new Country { CountryID = 6, Name = "Australia", Capital = "Canberra", Continent = "Oceania", FlagImagePath = "Flags/AU.png" });
            countries.Add(new Country { CountryID = 7, Name = "South Africa", Capital = "Pretoria", Continent = "Africa", FlagImagePath = "Flags/ZA.png" });


            return countries;
        }

        /// <summary>
        /// Retrieves all countries belonging to a specific continent.
        /// </summary>
        /// <param name="continent">The continent to filter by.</param>
        /// <returns>A list of countries from the specified continent. Returns an empty list if an error occurs or no countries are found.</returns>
        public List<Country> GetCountriesByContinent(string continent)
        {
            var countries = new List<Country>();
            Console.WriteLine($"DAL.CountryRepository.GetCountriesByContinent({continent}): Simulating database call.");

            if (string.IsNullOrWhiteSpace(continent))
            {
                return GetAllCountries(); // Or handle as an error/empty list
            }

            // Example of how it would work with actual MySql
            /*
            try
            {
                using (var connection = _dbConnector.GetActualConnection())
                {
                    connection.Open();
                    string query = "SELECT CountryID, Name, Capital, Continent, FlagImagePath FROM Countries WHERE Continent = @Continent;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Continent", continent);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countries.Add(new Country
                                {
                                    CountryID = reader.GetInt32("CountryID"),
                                    Name = reader.GetString("Name"),
                                    Capital = reader.GetString("Capital"),
                                    Continent = reader.GetString("Continent"),
                                    FlagImagePath = reader.GetString("FlagImagePath")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error fetching countries by continent '{continent}': {ex.Message}");
                // Log error, handle exception appropriately
            }
            */

            // Placeholder data for simulation
            var allCountries = GetAllCountries(); // Using the simulated full list
            foreach (var country in allCountries)
            {
                if (country.Continent.Equals(continent, StringComparison.OrdinalIgnoreCase))
                {
                    countries.Add(country);
                }
            }
            return countries;
        }
    }
}
