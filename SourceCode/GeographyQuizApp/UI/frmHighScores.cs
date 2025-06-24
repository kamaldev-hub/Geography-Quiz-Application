using GeographyQuizApp.BLL.Models;
using GeographyQuizApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GeographyQuizApp.UI
{
    public partial class frmHighScores : Form
    {
        private readonly QuizSessionRepository _quizSessionRepository;
        private readonly PlayerRepository _playerRepository; // To get player names
        private readonly Player? _currentPlayer; // For "My Scores" filter

        // Helper class for DataGridView display
        private class DisplayHighScore
        {
            public string PlayerName { get; set; } = string.Empty;
            public int Score { get; set; }
            public DateTime QuizTimestamp { get; set; }
            public string QuizType { get; set; } = string.Empty;
            public int TotalQuestions { get; set; }
        }

        public frmHighScores(QuizSessionRepository quizSessionRepository, PlayerRepository playerRepository, Player? currentPlayer)
        {
            InitializeComponent();
            _quizSessionRepository = quizSessionRepository ?? throw new ArgumentNullException(nameof(quizSessionRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _currentPlayer = currentPlayer;

            // Configure DataGridView
            dgvHighScores.AutoGenerateColumns = false;
            // Columns are defined in Designer.cs, ensure DataPropertyName is set for each.
        }

        private void frmHighScores_Load(object sender, EventArgs e)
        {
            rbMyScores.Enabled = _currentPlayer != null;
            if (_currentPlayer == null)
            {
                rbAllPlayers.Checked = true; // Default to all if no current player context
            }
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            List<QuizSession> scores;
            if (rbMyScores.Checked && _currentPlayer != null)
            {
                scores = _quizSessionRepository.GetScoresByPlayer(_currentPlayer.PlayerID);
            }
            else
            {
                scores = _quizSessionRepository.GetAllScores();
            }

            if (scores == null) {
                MessageBox.Show("Could not load high scores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                scores = new List<QuizSession>(); // Ensure it's not null for binding
            }

            // Populate Player names for display if not already part of QuizSession model
            // (QuizSessionRepository simulation might need adjustment for this)
            var displayScores = new List<DisplayHighScore>();
            var allPlayers = _playerRepository.GetAllPlayers(); // Get all players for name lookup

            foreach (var session in scores.OrderByDescending(s => s.Score).ThenByDescending(s => s.QuizTimestamp))
            {
                Player? player = null;
                if (session.Player != null) // If Player object is already populated in session
                {
                    player = session.Player;
                }
                else // Fallback to lookup by PlayerID
                {
                    player = allPlayers.FirstOrDefault(p => p.PlayerID == session.PlayerID);
                }

                displayScores.Add(new DisplayHighScore
                {
                    PlayerName = player?.PlayerName ?? "Unknown",
                    Score = session.Score,
                    QuizTimestamp = session.QuizTimestamp,
                    QuizType = session.QuizType,
                    TotalQuestions = session.TotalQuestions
                });
            }

            dgvHighScores.DataSource = displayScores;
        }

        private void rbFilter_CheckedChanged(object sender, EventArgs e)
        {
            // This event will fire for both radio buttons when one is changed.
            // We only need to act if the checked state actually changed to true for the sender.
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                LoadHighScores();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
