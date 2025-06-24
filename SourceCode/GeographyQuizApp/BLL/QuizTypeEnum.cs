namespace GeographyQuizApp.BLL
{
    /// <summary>
    /// Defines the different types of quizzes available.
    /// These names should correspond to what might be stored in the .ini file or selected by the user.
    /// </summary>
    public enum QuizTypeEnum
    {
        /// <summary>
        /// Guess the country given its flag.
        /// </summary>
        CountryFromFlag,

        /// <summary>
        /// Guess the capital city given the country name.
        /// </summary>
        CapitalFromCountry,

        /// <summary>
        /// Guess the flag given the country name.
        /// </summary>
        FlagFromCountry, // Question: Country Name, Answer: Flag Image Path (effectively country name to match path)

        /// <summary>
        /// Guess the country given its capital city.
        /// </summary>
        CountryFromCapital,

        /// <summary>
        /// Guess the capital city given the flag.
        /// </summary>
        CapitalFromFlag,

        /// <summary>
        /// Guess the flag given the capital city.
        /// </summary>
        FlagFromCapital // Question: Capital Name, Answer: Flag Image Path (effectively country name to match path)
    }
}
