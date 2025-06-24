using GeographyQuizApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
// using MySql.Data.MySqlClient; // Actual MySQL connector

namespace GeographyQuizApp.DAL
{
    /// <summary>
    /// Provides data access functionalities for QuizSession entities (high scores).
    /// </summary>
    public class QuizSessionRepository
    {
        private readonly DatabaseConnector _dbConnector;
        private static readonly List<QuizSession> _simulatedSessionsTable = new List<QuizSession>(); // For simulation
        private static int _nextSessionId = 1; // For simulation

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizSessionRepository"/> class.
        /// </summary>
        /// <param name="dbConnector">The database connector to use for database operations.</param>
        public QuizSessionRepository(DatabaseConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
        }

        /// <summary>
        /// Saves a quiz session to the database.
        /// </summary>
        /// <param name="session">The QuizSession object to save.</param>
        /// <returns>True if the session was saved successfully, false otherwise.</returns>
        public bool SaveSession(QuizSession session)
        {
            if (session == null)
            {
                Console.WriteLine("DAL.QuizSessionRepository.SaveSession(): Session object cannot be null.");
                return false;
            }
            if (session.Player == null && session.PlayerID <= 0)
            {
                 Console.WriteLine("DAL.QuizSessionRepository.SaveSession(): Session must have a valid Player or PlayerID.");
                return false;
            }

            Console.WriteLine($"DAL.QuizSessionRepository.SaveSession() for PlayerID {session.PlayerID}: Simulating database call.");

            // Simulation logic
            session.SessionID = _nextSessionId++;
            if(session.Player != null) session.PlayerID = session.Player.PlayerID; // Ensure PlayerID is set if Player object exists
            _simulatedSessionsTable.Add(session);
            Console.WriteLine($"Saved session ID {session.SessionID} for PlayerID {session.PlayerID}, Score {session.Score}.");
            return true;

            // Example of how it would work with actual MySql
            /*
            try
            {
                using (var connection = _dbConnector.GetActualConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO QuizSessions (PlayerID, Score, QuizTimestamp, TotalQuestions, QuizType) " +
                                   "VALUES (@PlayerID, @Score, @QuizTimestamp, @TotalQuestions, @QuizType);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PlayerID", session.Player?.PlayerID ?? session.PlayerID);
                        command.Parameters.AddWithValue("@Score", session.Score);
                        command.Parameters.AddWithValue("@QuizTimestamp", session.QuizTimestamp);
                        command.Parameters.AddWithValue("@TotalQuestions", session.TotalQuestions);
                        command.Parameters.AddWithValue("@QuizType", session.QuizType);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error saving session for PlayerID {session.Player?.PlayerID ?? session.PlayerID}: {ex.Message}");
                return false;
            }
            */
        }

        /// <summary>
        /// Retrieves all quiz sessions (scores) for a specific player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <returns>A list of quiz sessions for the specified player. Returns an empty list if an error occurs or no sessions are found.</returns>
        public List<QuizSession> GetScoresByPlayer(int playerId)
        {
            Console.WriteLine($"DAL.QuizSessionRepository.GetScoresByPlayer({playerId}): Simulating database call.");
            // Simulation
            return _simulatedSessionsTable.Where(s => s.PlayerID == playerId).OrderByDescending(s => s.QuizTimestamp).ToList();

            // Example of how it would work with actual MySql
            /*
            var sessions = new List<QuizSession>();
            try
            {
                using (var connection = _dbConnector.GetActualConnection())
                {
                    connection.Open();
                    // It's good practice to also retrieve player name if needed for display, or handle it in BLL
                    string query = "SELECT qs.SessionID, qs.PlayerID, qs.Score, qs.QuizTimestamp, qs.TotalQuestions, qs.QuizType, p.PlayerName " +
                                   "FROM QuizSessions qs INNER JOIN Players p ON qs.PlayerID = p.PlayerID " +
                                   "WHERE qs.PlayerID = @PlayerID ORDER BY qs.QuizTimestamp DESC;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PlayerID", playerId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var session = new QuizSession
                                {
                                    SessionID = reader.GetInt32("SessionID"),
                                    PlayerID = reader.GetInt32("PlayerID"),
                                    Score = reader.GetInt32("Score"),
                                    QuizTimestamp = reader.GetDateTime("QuizTimestamp"),
                                    TotalQuestions = reader.GetInt32("TotalQuestions"),
                                    QuizType = reader.GetString("QuizType"),
                                    Player = new Player // Populate player info
                                    {
                                        PlayerID = reader.GetInt32("PlayerID"),
                                        PlayerName = reader.GetString("PlayerName")
                                    }
                                };
                                sessions.Add(session);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error fetching scores for player ID {playerId}: {ex.Message}");
            }
            return sessions;
            */
        }

        /// <summary>
        /// Retrieves all quiz sessions (scores) from the database.
        /// </summary>
        /// <returns>A list of all quiz sessions. Returns an empty list if an error occurs or no sessions are found.</returns>
        public List<QuizSession> GetAllScores()
        {
            Console.WriteLine("DAL.QuizSessionRepository.GetAllScores(): Simulating database call.");
            // Simulation: Need to populate Player objects if they are not already linked.
            // For this simulation, the Player object within QuizSession might be null if only PlayerID was set.
            // A real app would join tables or fetch Player details separately.
            var allSessions = _simulatedSessionsTable.OrderByDescending(s => s.QuizTimestamp).ToList();

            // In a real scenario with joins, Player info would be part of the query.
            // For simulation, if we need full Player objects:
            // var playerRepo = new PlayerRepository(_dbConnector); // This creates a dependency issue or requires DI
            // var allPlayers = playerRepo.GetAllPlayers(); // Not ideal to call another repo here directly in simple simulation
            // foreach(var session in allSessions) {
            //    if(session.Player == null && session.PlayerID > 0) {
            //        session.Player = allPlayers.FirstOrDefault(p => p.PlayerID == session.PlayerID);
            //    }
            // }
            return allSessions;


            // Example of how it would work with actual MySql
            /*
            var sessions = new List<QuizSession>();
            try
            {
                using (var connection = _dbConnector.GetActualConnection())
                {
                    connection.Open();
                    string query = "SELECT qs.SessionID, qs.PlayerID, qs.Score, qs.QuizTimestamp, qs.TotalQuestions, qs.QuizType, p.PlayerName " +
                                   "FROM QuizSessions qs INNER JOIN Players p ON qs.PlayerID = p.PlayerID " +
                                   "ORDER BY qs.QuizTimestamp DESC;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var session = new QuizSession
                                {
                                    SessionID = reader.GetInt32("SessionID"),
                                    PlayerID = reader.GetInt32("PlayerID"),
                                    Score = reader.GetInt32("Score"),
                                    QuizTimestamp = reader.GetDateTime("QuizTimestamp"),
                                    TotalQuestions = reader.GetInt32("TotalQuestions"),
                                    QuizType = reader.GetString("QuizType"),
                                    Player = new Player
                                    {
                                        PlayerID = reader.GetInt32("PlayerID"),
                                        PlayerName = reader.GetString("PlayerName")
                                    }
                                };
                                sessions.Add(session);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error fetching all scores: {ex.Message}");
            }
            return sessions;
            */
        }
    }
}
