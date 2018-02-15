using UnityEngine;

using Gameflakes.TextMechanics;
using Gameflakes.HealthSystem;

namespace Gameflakes.TurtleType.TextModifications
{
    public sealed class TextManager : MonoBehaviour
    {
        private readonly string COLOR_LETTER_MATCH = "#a52a2aff";
        private readonly string WON_GAME_MESSAGE = "BOA CACETE, TU VENCEU!";
        private readonly string LOST_GAME_MESSAGE = "PORRA, TU PERDEU!";

        ITextHolder textController;
        ITextHolder healthController;
        ITextHolder timeController;

        // Text controller variables
        private string[ ] words;
        private int wordsGoneThrough;
        private int lettersRight;

        private void Awake ( )
        {
            textController = GameObject.Find ( "TextAnchor" ).GetComponent<TextController> ( );
            healthController = GameObject.Find ( "Health" ).GetComponent<TextController> ( );
            timeController = GameObject.Find ( "Time" ).GetComponent<TextController> ( );
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

            holderReturn = "<color=" + COLOR_LETTER_MATCH + ">";
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

        public void FinishGameMessage ( )
        {
            textController.UpdateTextContent ( WON_GAME_MESSAGE );
        }

        public void LostGameMessage ( )
        {
            textController.UpdateTextContent ( LOST_GAME_MESSAGE );
        }

        public void HealthUpdate ( int health )
        {
            healthController.UpdateTextContent ( health + "" );
        }

        public void UpdateTime ( float time )
        {
            if ( time < 0.0f )
            {
                time = 0.0f;
            }

            timeController.UpdateTextContent ( time.ToString ( "0.00" ) );
        }
    }
}
