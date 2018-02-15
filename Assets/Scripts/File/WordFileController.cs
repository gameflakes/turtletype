using UnityEngine;
using System.IO;

namespace Gameflakes.TurtleType.FileController
{
    public sealed class WordFileController : IWordFileReader
    {
        private WordFileController ( ) { }
        private static readonly IWordFileReader singletonInstance = new WordFileController ( );
        public static IWordFileReader GetSingletonInstance ( )
        {
            return singletonInstance;
        }

        private static readonly string BASE_PATH = Application.dataPath + "/Words/";

        private static readonly int MIN_NUMBER_OF_LETTERS = 4;
        private static readonly int MAX_NUMBER_OF_LETTERS = 8;

        private static readonly int MIN_NUMBER_OF_WORDS = 1;
        private static readonly int MAX_NUMBER_OF_WORDS = 500;

        // doc. on IWordFileReader
        public string[ ] ReadSpecificFileAndGetRandomWords ( int numberOfWords, int numberOfLetters )
        {
            if ( numberOfLetters < MIN_NUMBER_OF_LETTERS || numberOfLetters > MAX_NUMBER_OF_LETTERS )
            {
                throw new System.ArgumentOutOfRangeException ( numberOfLetters + " is out of range." );
            }

            if ( numberOfWords < MIN_NUMBER_OF_WORDS || numberOfWords > MAX_NUMBER_OF_WORDS )
            {
                throw new System.ArgumentOutOfRangeException ( numberOfWords + " is out of range." );
            }

            int[ ] indexes = GetRandomNumbersBasedOnIntervals ( numberOfWords, MIN_NUMBER_OF_WORDS - 1, MAX_NUMBER_OF_WORDS - 1 );
            string[ ] words = new string[ numberOfWords ];

            string fileName = GetFileNameByNumberOfLetters ( numberOfLetters );
            string[ ] lines = File.ReadAllLines ( BASE_PATH + fileName );

            for ( int i = 0; i < indexes.Length; i++ )
            {
                words[ i ] = lines[ indexes[ i ] ].ToLower ( );
            }

            return words;
        }

 
        /// <summary>
        /// It is used to retrieve an array of n non repeated random integer numbers,
        /// that respects a range given by start and end (inclusives).
        /// </summary>
        /// 
        /// <param name="numberOfRandomNumbersDesired">
        /// An integer that represents how many random numbers do you want.
        /// </param>
        /// <param name="start">
        /// An integer to set the start value of the random number's range (Inclusive).
        /// </param>
        /// <param name="end">
        /// An integer to set the end value of the random number's range (Inclusive).
        /// </param>
        /// 
        /// <returns>
        /// Returns a array of ints, which contains the random numbers
        /// </returns>
        /// 
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException when any parameters are less or equal than 0;
        ///   ||               ||              when start is smaller than end;
        ///   ||               ||              when the numberOfRandomNumbersDesired is greater than the range desired.
        /// </exception>
        private int [ ] GetRandomNumbersBasedOnIntervals ( int numberOfRandomNumbersDesired, int start, int end )
        {
            if ( numberOfRandomNumbersDesired <= 0 || start < 0 || end < 0 )
            {
                throw new System.ArgumentOutOfRangeException ( "A parameter is out of range." );
            }

            if ( start >= end )
            {
                throw new System.ArgumentOutOfRangeException ( start + " should be greater than or equal to " + end + ".");
            }

            if ( numberOfRandomNumbersDesired > (end - start + 1) )
            {
                throw new System.ArgumentOutOfRangeException ( numberOfRandomNumbersDesired + " should be less than or equal to (" + start + " + " + end + ")." );
            }

            int [ ] numbers = new int [ numberOfRandomNumbersDesired ];

            int numberOfPossibilities = end - start + 1;
            int offsetOfIntervals = numberOfPossibilities / numberOfRandomNumbersDesired;

            for ( int i = 0; i < numberOfRandomNumbersDesired; i++ )
            {
                int endOfRange = start + offsetOfIntervals;
                while ( endOfRange > end ) endOfRange--;

                numbers [ i ] = (int) Random.Range ( start, endOfRange );

                start += offsetOfIntervals;
            }

            return numbers;
        }


        /// <summary>
        /// It is used to get a file's name based on
        /// the number of letters that the file's words should have.
        /// </summary>
        /// 
        /// <param name="numberOfLetters">
        /// An integer that defines how many letters the file's words should have.
        /// </param>
        /// 
        /// <returns>
        /// Returns a string which represents the file's name.
        /// </returns>
        /// 
        /// <exception cref="CustomException.FileNotFoundException">
        /// Throws FileNotFoundException when the numberOfLetters doesn't match any file.
        /// </exception>
        private string GetFileNameByNumberOfLetters ( int numberOfLetters ) 
        {
            switch ( numberOfLetters )
            {
                case 4:
                    return "fourLetterWords.txt";
                case 5:
                    return "fiveLetterWords.txt";
                case 6:
                    return "sixLetterWords.txt";
                case 7:
                    return "sevenLetterWords.txt";
                case 8:
                    return "eightLetterWords.txt";
            }

            throw new CustomException.FileNotFoundException ( numberOfLetters + " letters words' list not found." );
        }
    }
}
