
namespace Gameflakes.TurtleType.FileController
{
    public interface IFileWordReader
    {
        /// <summary>
        /// It is used to retrieve an array of n non repeated words,
        /// that are on the file matched by it's words length.
        /// </summary>
        /// 
        /// <param name="numberOfLetters">
        /// An integer that represents how many letters should the file's words have.
        /// </param>
        /// <param name="numberOfWords">
        /// An integer that represents how many random words are going to be returned.
        /// </param>
        /// 
        /// <returns>
        /// Returns an array of n strings.
        /// The n size is defined by the numberOfWords given. 
        /// </returns>
        /// 
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException when the numberOfLetters or numberOfWords
        /// is out of the range defined by this class' constants (MIN and MAX).
        /// </exception>
        string[ ] ReadSpecificFileAndGetRandomWords ( int numberOfLetters, int numberOfWords );
    }
}
