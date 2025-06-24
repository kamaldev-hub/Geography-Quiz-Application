```mermaid
erDiagram
    Players ||--o{ QuizSessions : "has many"
    Players {
        INT PlayerID PK
        VARCHAR(50) PlayerName UNIQUE
        TIMESTAMP CreatedAt
    }
    Countries {
        INT CountryID PK
        VARCHAR(100) Name
        VARCHAR(100) Capital
        ENUM Continent
        VARCHAR(255) FlagImagePath
    }
    QuizSessions {
        INT SessionID PK
        INT PlayerID FK
        INT Score
        TIMESTAMP QuizTimestamp
        INT TotalQuestions
        VARCHAR(50) QuizType
    }
```

**Entities and Attributes:**

*   **Players**
    *   `PlayerID` (PK, INT, AUTO_INCREMENT)
    *   `PlayerName` (VARCHAR(50), UNIQUE)
    *   `CreatedAt` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)
*   **Countries**
    *   `CountryID` (PK, INT, AUTO_INCREMENT)
    *   `Name` (VARCHAR(100), NOT NULL)
    *   `Capital` (VARCHAR(100), NOT NULL)
    *   `Continent` (ENUM('Asia', 'Europe', 'Africa', 'North America', 'South America', 'Oceania'), NOT NULL)
    *   `FlagImagePath` (VARCHAR(255), NOT NULL) - Path to the flag image file relative to the application executable.
*   **QuizSessions**
    *   `SessionID` (PK, INT, AUTO_INCREMENT)
    *   `PlayerID` (FK to Players.PlayerID)
    *   `Score` (INT, NOT NULL)
    *   `QuizTimestamp` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)
    *   `TotalQuestions` (INT, NOT NULL)
    *   `QuizType` (VARCHAR(50), NOT NULL) - e.g., "Country from Flag"

**Relationship:**

*   A `Player` can have many `QuizSessions` (One-to-Many).
*   (Implicit) `QuizSessions` might relate to questions derived from `Countries`, but the questions themselves are not stored as a separate entity in the database for this design. Each session has a score based on answers to questions generated on-the-fly from the `Countries` table.
