using GeographyQuizApp.BLL.Models;
using System;
using System.Collections.Generic;
// using MySql.Data.MySqlClient; // Actual MySQL connector

namespace GeographyQuizApp.DAL
{
    /// <summary>
    /// Provides data access functionalities for Player entities.
    /// </summary>
    public class PlayerRepository
    {
        private readonly DatabaseConnector _dbConnector;
        private static readonly List<Player> _simulatedPlayersTable = new List<Player>(); // For simulation
        private static int _nextPlayerId = 1; // For simulation

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="dbConnector">The database connector to use for database operations.</param>
        public PlayerRepository(DatabaseConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
        }

        /// <summary>
        /// Gets a player by name. If the player does not exist, creates a new player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <returns>The existing or newly created Player object. Returns null if the name is invalid or an error occurs.</returns>
        public Player? GetOrCreatePlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("DAL.PlayerRepository.GetOrCreatePlayer(): Player name cannot be empty.");
                return null;
            }

            Console.WriteLine($"DAL.PlayerRepository.GetOrCreatePlayer('{name}'): Simulating database call.");

            // Simulation logic
            Player? existingPlayer = _simulatedPlayersTable.Find(p => p.PlayerName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingPlayer != null)
            {
                Console.WriteLine($"Found existing player: {name}");
                return existingPlayer;
            }
            else
            {
                var newPlayer = new Player
                {
                    PlayerID = _nextPlayerId++,
                    PlayerName = name,
                    CreatedAt = DateTime.UtcNow
                };
                _simulatedPlayersTable.Add(newPlayer);
                Console.WriteLine($"Created new player: {name} with ID {newPlayer.PlayerID}");
                return newPlayer;
            }

            // Example of how it would work with actual MySql
            /*
            try
            {
                using (var connection = _dbConnector.GetActualConnection())
                {
                    connection.Open();
                    // Check if player exists
                    string selectQuery = "SELECT PlayerID, PlayerName, CreatedAt FROM Players WHERE PlayerName = @PlayerName;";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@PlayerName", name);

                    Player player = null;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            player = new Player
                            {
                                PlayerID = reader.GetInt32("PlayerID"),
                                PlayerName = reader.GetString("PlayerName"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            };
                        }
                    }

                    if (player != null)
                    {
                        return player;
                    }
                    else
                    {
                        // Player does not exist, create new
                        string insertQuery = "INSERT INTO Players (PlayerName) VALUES (@PlayerName); SELECT LAST_INSERT_ID();";
                        command.CommandText = insertQuery;
                        // command.Parameters.Clear(); // Parameters are already set if using the same name
                        // command.Parameters.AddWithValue("@PlayerName", name); // Ensure it's there

                        object newPlayerIdObj = command.ExecuteScalar(); // Gets the new PlayerID
                        if (newPlayerIdObj != null && newPlayerIdObj != DBNull.Value)
                        {
                             return new Player { PlayerID = Convert.ToInt32(newPlayerIdObj), PlayerName = name, CreatedAt = DateTime.UtcNow };
                        }
                        return null; // Insert failed
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error in GetOrCreatePlayer for '{name}': {ex.Message}");
                return null;
            }
            */
        }

        /// <summary>
        /// Retrieves all players from the database.
        /// </summary>
        /// <returns>A list of all players. Returns an empty list if an error occurs or no players are found.</returns>
        public List<Player> GetAllPlayers()
        {
            Console.WriteLine("DAL.PlayerRepository.GetAllPlayers(): Simulating database call.");
            // Return a copy to prevent external modification of the simulated table
            return new List<Player>(_simulatedPlayersTable);

            // Example of how it would work with actual MySql
            /*
            var players = new List<Player>();
            try
            {
                using (var connection = _dbConnector.GetActualConnection())
                {
                    connection.Open();
                    string query = "SELECT PlayerID, PlayerName, CreatedAt FROM Players;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                players.Add(new Player
                                {
                                    PlayerID = reader.GetInt32("PlayerID"),
                                    PlayerName = reader.GetString("PlayerName"),
                                    CreatedAt = reader.GetDateTime("CreatedAt")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error fetching all players: {ex.Message}");
                // Log error
            }
            return players;
            */
        }
    }
}
