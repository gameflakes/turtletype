
namespace Gameflakes.TurtleType.TextController
{
    public interface ITextHolder
    {
        /// <summary>
        /// Update the class Text element's content.
        /// It does not check if the text is being displayed, neither the opposite.
        /// </summary>
        /// 
        /// <param name="newText">
        /// It's a string, that contains the text that
        /// is going to be used to update the class Text element's content;
        /// </param>
        /// 
        /// <exception cref="System.ArgumentNullException">
        /// Throws an exception if the string ref is null.
        /// </exception> 
        void UpdateTextContent ( string newText );

        /// <summary>
        /// Get the class Text element's content.
        /// </summary>
        /// 
        /// <returns>
        /// Returns the class Text element's content in a string format.
        /// </returns>
        string GetTextContent ( );
    }
}
