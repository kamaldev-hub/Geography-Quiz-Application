using GeographyQuizAvalonia.Models; // Updated namespace for Model
using System;
using System.Collections.Generic;
using System.Linq;

// using MySql.Data.MySqlClient; // Actual MySQL connector (not used in simulation)

namespace GeographyQuizAvalonia.Services // Changed namespace
{
    /// <summary>
    /// Provides data access functionalities for QuizSession entities (high scores) (simulated).
    /// </summary>
    public class QuizSessionRepository
    {
        private readonly DatabaseConnector _dbConnector;
        private static readonly List<QuizSession> _simulatedSessionsTable = new List<QuizSession>();
        private static int _nextSessionId = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizSessionRepository"/> class.
        /// </summary>
        /// <param name="dbConnector">The database connector to use for database operations.</param>
        public QuizSessionRepository(DatabaseConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
        }

        /// <summary>
        /// Saves a quiz session to the database (simulated).
        /// </summary>
        /// <param name="session">The QuizSession object to save.</param>
        /// <returns>True if the session was saved successfully, false otherwise.</returns>
        public bool SaveSession(QuizSession session)
        {
            if (session == null)
            {
                Console.WriteLine("Services.QuizSessionRepository.SaveSession(): Session object cannot be null.");
                return false;
            }
            if (session.Player == null && session.PlayerID <= 0) // PlayerID must be valid if Player obj is not set
            {
                 Console.WriteLine("Services.QuizSessionRepository.SaveSession(): Session must have a valid Player or PlayerID.");
                return false;
            }

            Console.WriteLine($"Services.QuizSessionRepository.SaveSession() for PlayerID {session.PlayerID}: Simulating data operation.");

            session.SessionID = _nextSessionId++;
            if(session.Player != null) session.PlayerID = session.Player.PlayerID;

            _simulatedSessionsTable.Add(session);
            Console.WriteLine($"Saved session ID {session.SessionID} for PlayerID {session.PlayerID}, Score {session.Score}.");
            return true;
        }

        /// <summary>
        /// Retrieves all quiz sessions (scores) for a specific player (simulated).
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <returns>A list of quiz sessions for the specified player.</returns>
        public List<QuizSession> GetScoresByPlayer(int playerId)
        {
            Console.WriteLine($"Services.QuizSessionRepository.GetScoresByPlayer({playerId}): Simulating data retrieval.");
            return _simulatedSessionsTable.Where(s => s.PlayerID == playerId)
                                          .OrderByDescending(s => s.QuizTimestamp)
                                          .ToList();
        }

        /// <summary>
        /// Retrieves all quiz sessions (scores) from the database (simulated).
        /// </summary>
        /// <returns>A list of all quiz sessions.</returns>
        public List<QuizSession> GetAllScores()
        {
            Console.WriteLine("Services.QuizSessionRepository.GetAllScores(): Simulating data retrieval.");
            // For simulation, the Player object within QuizSession might be null.
            // The ViewModel or service layer using this would be responsible for populating full Player details if needed for display,
            // typically by also fetching from PlayerRepository.
            return _simulatedSessionsTable.OrderByDescending(s => s.Score)
                                          .ThenByDescending(s => s.QuizTimestamp)
                                          .ToList();
        }
    }
}
