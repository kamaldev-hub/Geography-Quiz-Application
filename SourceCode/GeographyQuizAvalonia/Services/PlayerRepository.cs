using GeographyQuizAvalonia.Models; // Updated namespace for Model
using System;
using System.Collections.Generic;
using System.Linq; // Added for Find

// using MySql.Data.MySqlClient; // Actual MySQL connector (not used in simulation)

namespace GeographyQuizAvalonia.Services // Changed namespace
{
    /// <summary>
    /// Provides data access functionalities for Player entities (simulated).
    /// </summary>
    public class PlayerRepository
    {
        private readonly DatabaseConnector _dbConnector;
        private static readonly List<Player> _simulatedPlayersTable = new List<Player>();
        private static int _nextPlayerId = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="dbConnector">The database connector to use for database operations.</param>
        public PlayerRepository(DatabaseConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
        }

        /// <summary>
        /// Gets a player by name. If the player does not exist, creates a new player (simulated).
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <returns>The existing or newly created Player object. Returns null if the name is invalid.</returns>
        public Player? GetOrCreatePlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Services.PlayerRepository.GetOrCreatePlayer(): Player name cannot be empty.");
                return null; // Or throw ArgumentException
            }

            Console.WriteLine($"Services.PlayerRepository.GetOrCreatePlayer('{name}'): Simulating data operation.");

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
        }

        /// <summary>
        /// Retrieves all players from the database (simulated).
        /// </summary>
        /// <returns>A list of all players.</returns>
        public List<Player> GetAllPlayers()
        {
            Console.WriteLine("Services.PlayerRepository.GetAllPlayers(): Simulating data retrieval.");
            return new List<Player>(_simulatedPlayersTable); // Return a copy
        }
    }
}
