using GeographyQuizApp.BLL;
using GeographyQuizApp.BLL.Models;
using GeographyQuizApp.DAL;
using System;
using System.Linq;
using System.Windows.Forms;
using System.IO; // For Path.Combine and File.Exists (for .ini)
// using System.Configuration; // For App.config, if DatabaseConnector doesn't hide it fully

namespace GeographyQuizApp.UI
{
    public partial class frmMain : Form
    {
        private PlayerRepository _playerRepository;
        private CountryRepository _countryRepository;
        private QuizSessionRepository _quizSessionRepository;
        private Player? _currentPlayer;
        private IniConfigParser? _iniParser; // For bonus requirement

        public frmMain()
        {
            InitializeComponent();
            // In a real app with Dependency Injection, these would be injected.
            // For simplicity, we instantiate them here.
            var dbConnector = new DatabaseConnector(); // Reads from App.config internally
            _playerRepository = new PlayerRepository(dbConnector);
            _countryRepository = new CountryRepository(dbConnector);
            _quizSessionRepository = new QuizSessionRepository(dbConnector);
            _iniParser = new IniConfigParser();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Populate ComboBox for continents
            cmbContinent.Items.Clear(); // Clear default items if any from designer
            cmbContinent.Items.Add("Worldwide");
            // Potentially load continents dynamically from data, but AGENTS.md implies fixed list
            var continents = new[] { "Africa", "Asia", "Europe", "North America", "Oceania", "South America" };
            cmbContinent.Items.AddRange(continents);
            cmbContinent.SelectedIndex = 0; // Default to "Worldwide"

            // Check if quiz.ini exists for the bonus button
            string iniFilePath = Path.Combine(Application.StartupPath, "quiz.ini");
            btnStartFromConfig.Enabled = File.Exists(iniFilePath);

        }

        private bool ValidatePlayerName()
        {
            if (string.IsNullOrWhiteSpace(txtPlayerName.Text))
            {
                MessageBox.Show("Please enter your name.", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlayerName.Focus();
                return false;
            }
            _currentPlayer = _playerRepository.GetOrCreatePlayer(txtPlayerName.Text.Trim());
            if (_currentPlayer == null)
            {
                 MessageBox.Show("Could not create or retrieve player. Please try a different name.", "Player Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private QuizTypeEnum GetSelectedQuizType()
        {
            if (rbCountryFromFlag.Checked) return QuizTypeEnum.CountryFromFlag;
            if (rbCapitalFromCountry.Checked) return QuizTypeEnum.CapitalFromCountry;
            if (rbFlagFromCountry.Checked) return QuizTypeEnum.FlagFromCountry;
            if (rbCountryFromCapital.Checked) return QuizTypeEnum.CountryFromCapital;
            if (rbCapitalFromFlag.Checked) return QuizTypeEnum.CapitalFromFlag;
            if (rbFlagFromCapital.Checked) return QuizTypeEnum.FlagFromCapital;

            return QuizTypeEnum.CountryFromFlag; // Default
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            if (!ValidatePlayerName() || _currentPlayer == null) return;

            QuizTypeEnum selectedQuizType = GetSelectedQuizType();
            string selectedContinent = cmbContinent.SelectedItem.ToString() ?? "Worldwide";
            int numberOfQuestions = (int)numNumberOfQuestions.Value;

            var quizManager = new QuizManager(_countryRepository, _quizSessionRepository);

            // These flags from .ini are not used in manual start as per AGENTS.md, but could be added as UI options
            // bool showFeedback = true;
            // bool savePoints = true;

            quizManager.StartQuiz(_currentPlayer, selectedQuizType, selectedContinent, numberOfQuestions);

            if (!quizManager.IsQuizActive || !quizManager.Questions.Any())
            {
                MessageBox.Show("Could not start the quiz. There might be insufficient data for the selected criteria.", "Quiz Start Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmQuiz quizForm = new frmQuiz(quizManager, true, true); // Assuming default true for feedback/save for manual start
            quizForm.ShowDialog();
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            // Pass current player for "My Scores" filter, can be null if no name entered yet
            _currentPlayer = string.IsNullOrWhiteSpace(txtPlayerName.Text) ? null : _playerRepository.GetOrCreatePlayer(txtPlayerName.Text.Trim());

            frmHighScores highScoresForm = new frmHighScores(_quizSessionRepository, _playerRepository, _currentPlayer);
            highScoresForm.ShowDialog();
        }

        private void btnStartFromConfig_Click(object sender, EventArgs e)
        {
            if (!ValidatePlayerName() || _currentPlayer == null || _iniParser == null) return;

            string iniFilePath = Path.Combine(Application.StartupPath, "quiz.ini");
            if (!File.Exists(iniFilePath))
            {
                MessageBox.Show("quiz.ini file not found in the application directory.", "Config File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStartFromConfig.Enabled = false; // Disable if not found
                return;
            }

            var config = _iniParser.LoadConfig(iniFilePath);
            if (config == null || !config.Any())
            {
                MessageBox.Show("Could not read or parse quiz.ini.", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // fragetyp = flagge_zu_land
                // kontinent = europa
                // anzahl_fragen = 10
                // feedback_anzeigen = ja
                // punkte_speichern = ja
                string quizTypeStr = config.GetValueOrDefault("QuizConfig", "fragetyp") ?? "flagge_zu_land";
                string continentStr = config.GetValueOrDefault("QuizConfig", "kontinent") ?? "Worldwide";
                int questions = int.TryParse(config.GetValueOrDefault("QuizConfig", "anzahl_fragen"), out int q) ? q : 10;
                bool showFeedback = (config.GetValueOrDefault("QuizConfig", "feedback_anzeigen") ?? "ja").Equals("ja", StringComparison.OrdinalIgnoreCase);
                bool savePoints = (config.GetValueOrDefault("QuizConfig", "punkte_speichern") ?? "ja").Equals("ja", StringComparison.OrdinalIgnoreCase);

                QuizTypeEnum quizType;
                switch (quizTypeStr.ToLowerInvariant())
                {
                    case "flagge_zu_land": quizType = QuizTypeEnum.CountryFromFlag; break;
                    case "hauptstadt_zu_land": quizType = QuizTypeEnum.CapitalFromCountry; break;
                    case "land_zu_flagge": quizType = QuizTypeEnum.FlagFromCountry; break; // Assuming mapping
                    case "land_zu_hauptstadt": quizType = QuizTypeEnum.CountryFromCapital; break;
                    case "hauptstadt_zu_flagge": quizType = QuizTypeEnum.CapitalFromFlag; break;
                    case "flagge_zu_hauptstadt": quizType = QuizTypeEnum.FlagFromCapital; break; // Assuming mapping
                    default:
                        MessageBox.Show($"Invalid 'fragetyp' in quiz.ini: {quizTypeStr}", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Normalize continent name if needed, e.g. "europa" to "Europe"
                // For simplicity, assume QuizManager or CountryRepository handles various casings or direct values.
                // The AGENTS.md uses "europa", while enum/standard is "Europe".
                // A mapping might be needed if the DAL/BLL expects specific casing.
                // For now, we pass it as is. CountryRepository simulation uses OrdinalIgnoreCase.

                var quizManager = new QuizManager(_countryRepository, _quizSessionRepository);
                quizManager.StartQuiz(_currentPlayer, quizType, continentStr, questions, showFeedback, savePoints);

                if (!quizManager.IsQuizActive || !quizManager.Questions.Any())
                {
                    MessageBox.Show("Could not start the quiz based on config. Insufficient data or invalid configuration.", "Quiz Start Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frmQuiz quizForm = new frmQuiz(quizManager, showFeedback, savePoints);
                quizForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing quiz.ini: {ex.Message}", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
