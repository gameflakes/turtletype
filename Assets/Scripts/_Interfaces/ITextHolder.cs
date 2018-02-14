
namespace Gameflakes.TextMechanics
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
        /// <returns>
        /// Returns the replaced Text element's content.
        /// </returns>
        /// 
        /// <exception cref="System.ArgumentNullException">
        /// Throws an exception if the string reference is null.
        /// </exception> 
        string UpdateTextContent ( string newText );

        /// <summary>
        /// Clear the class Text element's content.
        /// </summary>
        /// 
        /// <returns>
        /// Returns the replaced Text element's content.
        /// </returns>
        string ClearTextContent ( );

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
