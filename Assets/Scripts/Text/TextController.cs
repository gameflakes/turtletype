using UnityEngine;
using UnityEngine.UI;

namespace Gameflakes.TextMechanics
{
    public sealed class TextController : MonoBehaviour, ITextHolder
    {
        private static readonly string DEFAULT_MESSAGE = "loading...";
        private Text textAnchor;

        public string UpdateTextContent ( string newText )
        {
            if ( newText == null )
            {
                throw new System.ArgumentNullException ( );
            }

            string holder = textAnchor.text;

            if ( newText == "" )
            {
                textAnchor.text = "";
                return holder;
            }

            textAnchor.text = string.Copy ( newText );
            return holder;
        }

        public string ClearTextContent ( )
        {
            return UpdateTextContent ( "" );
        }

        public string GetTextContent ( )
        {
            return string.Copy ( textAnchor.text );
        }

        // Called only once in a game's lifetime.
        private void Awake ( )
        {
            InitializeReferenceAndSetMessageToDefaultMessage ( );
        }

        private void InitializeReferenceAndSetMessageToDefaultMessage ( )
        {
            textAnchor = GetComponent<Text> ( );
            textAnchor.text = DEFAULT_MESSAGE;
        }
    }
}
