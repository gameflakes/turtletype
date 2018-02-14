using UnityEngine;

using Gameflakes.TextMechanics;
using System;

namespace Gameflakes.TurtleType.TextModifications
{
    public sealed class TextManager : MonoBehaviour
    {
        private readonly string colorLetterMatch = "#a52a2aff";

        private string[] words;
        ITextHolder textController; /* private void seeMethods ( )
                                     * {
                                     *   textController.ClearTextContent ( );
                                     *   textController.GetTextContent ( );
                                     *   textController.UpdateTextContent ( "string" );
                                     * }
                                     */
        private int wordsGoneThrough;
        private int lettersRight;
        
        private void Awake ( )
        {
            textController = GetComponent<TextController> ( );
        }

        public void SetLevelWords ( string[ ] words )
        {
            this.words = words;
            wordsGoneThrough = 0;
        }

        public void UpdateShowingWord ( )
        {
            lettersRight = 0;
            textController.UpdateTextContent ( words[ wordsGoneThrough ] );
            wordsGoneThrough++;
        }

        // Paint the next letter!
        public void GotNextLetterRight ( )
        {
            lettersRight++;

            string holderReturn = words[ wordsGoneThrough - 1 ];
            string holderNotPainted = holderReturn.Substring ( lettersRight );
            char[] holderChar = holderReturn.ToCharArray ( );

            holderReturn = "<color=" + colorLetterMatch + ">";
            for ( int i = 0; i < lettersRight; i++ )
            {
                holderReturn += holderChar[i];
            }
            holderReturn += "</color>";

            holderReturn += holderNotPainted;
            textController.UpdateTextContent( holderReturn );
        }

        public void ResetWord ( )
        {
            lettersRight = 0;
            textController.UpdateTextContent ( words[ wordsGoneThrough - 1 ] );
        }
    }
}
