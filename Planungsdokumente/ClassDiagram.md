```mermaid
classDiagram
    namespace DAL {
        class DatabaseConnector {
            -connectionString : string
            +GetConnection() : MySqlConnection
        }
        class CountryRepository {
            +GetAllCountries() : List<Country>
            +GetCountriesByContinent(continent : string) : List<Country>
        }
        class PlayerRepository {
            +GetOrCreatePlayer(name : string) : Player
            +GetAllPlayers() : List<Player>
        }
        class QuizSessionRepository {
            +SaveSession(session : QuizSession) : void
            +GetScoresByPlayer(playerId : int) : List<QuizSession>
            +GetAllScores() : List<QuizSession>
        }
        DatabaseConnector --|> CountryRepository : uses
        DatabaseConnector --|> PlayerRepository : uses
        DatabaseConnector --|> QuizSessionRepository : uses
    }

    namespace BLL {
        namespace Models {
            class Player {
                +PlayerID : int
                +PlayerName : string
            }
            class Country {
                +CountryID : int
                +Name : string
                +Capital : string
                +Continent : string // Representing ENUM
                +FlagImagePath : string
            }
            class QuizSession {
                +SessionID : int
                +Player : Player
                +Score : int
                +QuizTimestamp : DateTime
                +TotalQuestions : int
                +QuizType : string // Or QuizTypeEnum
            }
            class Question {
                +QuestionText : string
                +CorrectAnswer : string
                +Options : List<string>
                +QuestionImage : object // Bitmap or Image
            }
        }
        class QuizTypeEnum {
            <<enumeration>>
            CountryFromFlag
            CapitalFromCountry
            FlagFromCountry
            CountryFromCapital
            CapitalFromFlag
            FlagFromCapital
        }
        class QuizManager {
            +CurrentPlayer : Player
            +CurrentQuizType : QuizTypeEnum
            +Questions : List<Models.Question>
            +CurrentQuestionIndex : int
            +Score : int
            +StartQuiz(type : QuizTypeEnum, continent : string, questionCount : int) : void
            +GetNextQuestion() : Models.Question
            +SubmitAnswer(answer : string) : bool
            +SaveResult() : void
        }
        QuizManager o-- Models.Player : currentPlayer
        QuizManager o-- Models.Question : questions
        QuizManager ..> CountryRepository : uses
        QuizManager ..> QuizSessionRepository : uses
        Models.QuizSession o-- Models.Player : player
    }

    namespace UI {
        class frmMain {
            -txtPlayerName : TextBox
            -radioQuizType : RadioButton[]
            -cmbContinent : ComboBox
            -btnStartQuiz : Button
            -btnHighScores : Button
            -btnStartFromConfig : Button
            +frmMain()
            -btnStartQuiz_Click()
            -btnHighScores_Click()
            -btnStartFromConfig_Click()
        }
        class frmQuiz {
            -lblQuestionNumber : Label
            -lblScore : Label
            -picQuestionImage : PictureBox
            -lblQuestionText : Label
            -btnOption1 : Button
            -btnOption2 : Button
            -btnOption3 : Button
            -btnOption4 : Button
            -lblFeedback : Label
            -progressBarQuiz : ProgressBar
            -quizManager : BLL.QuizManager
            +frmQuiz(quizManager : BLL.QuizManager)
            -DisplayQuestion() : void
            -AnswerButton_Click() : void
        }
        class frmHighScores {
            -dgvHighScores : DataGridView
            -rbAllPlayers : RadioButton
            -rbMyScores : RadioButton
            -btnClose : Button
            +frmHighScores()
            -LoadScores() : void
        }
        frmMain ..> BLL.QuizManager : creates/uses
        frmMain ..> BLL.PlayerRepository : uses
        frmMain ..> frmQuiz : shows
        frmMain ..> frmHighScores : shows
        frmQuiz ..> BLL.QuizManager : uses
        frmHighScores ..> BLL.QuizSessionRepository : uses
    }
```

**DAL Namespace:**

*   `DatabaseConnector`: Manages MySQL connection.
*   `CountryRepository`: CRUD for Countries.
*   `PlayerRepository`: CRUD for Players.
*   `QuizSessionRepository`: CRUD for QuizSessions.

**BLL Namespace:**

*   **Models (sub-namespace):**
    *   `Player`: Player data.
    *   `Country`: Country data.
    *   `QuizSession`: Quiz session data.
    *   `Question`: Represents a single quiz question.
*   `QuizType` (Enum): Defines types of quizzes.
*   `QuizManager`: Core quiz logic (generating questions, scoring).

**UI Namespace (Windows Forms):**

*   `frmMain`: Main menu, quiz selection.
*   `frmQuiz`: Quiz gameplay window.
*   `frmHighScores`: High scores display.
