-- Create the database
CREATE DATABASE IF NOT EXISTS `geography_quiz_db`;
USE `geography_quiz_db`;

-- Table for Players
CREATE TABLE `Players` (
  `PlayerID` INT NOT NULL AUTO_INCREMENT,
  `PlayerName` VARCHAR(50) NOT NULL,
  `CreatedAt` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PlayerID`),
  UNIQUE INDEX `PlayerName_UNIQUE` (`PlayerName` ASC) VISIBLE);

-- Table for Countries
CREATE TABLE `Countries` (
  `CountryID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Capital` VARCHAR(100) NOT NULL,
  `Continent` ENUM('Asia', 'Europe', 'Africa', 'North America', 'South America', 'Oceania') NOT NULL,
  `FlagImagePath` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`CountryID`));

-- Table for Quiz Sessions (Highscores)
CREATE TABLE `QuizSessions` (
  `SessionID` INT NOT NULL AUTO_INCREMENT,
  `PlayerID` INT NOT NULL,
  `Score` INT NOT NULL,
  `QuizTimestamp` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  `TotalQuestions` INT NOT NULL,
  `QuizType` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`SessionID`),
  INDEX `FK_Player_idx` (`PlayerID` ASC) VISIBLE,
  CONSTRAINT `FK_Player`
    FOREIGN KEY (`PlayerID`)
    REFERENCES `Players` (`PlayerID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);
