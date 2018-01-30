using UnityEngine;
using UnityEngine.UI;

namespace Gameflakes.TurtleType.TextController
{
    public sealed class TextController : MonoBehaviour, ITextHolder
    {
        private static readonly string DEFAULT_MESSAGE = "loading...";
        private Text textAnchor;

        public void UpdateTextContent ( string newText )
        {
            if ( newText == null )
            {
                throw new System.ArgumentNullException ( );
            }

            textAnchor.text = newText;
        }

        public string GetTextContent ( )
        {
            return textAnchor.text;
        }

        // Called only once in a game's lifetime.
        private void Awake ( )
        {
            InitializeReferenceAndSetMessageToDefaultMessage();
        }

        private void InitializeReferenceAndSetMessageToDefaultMessage ( )
        {
            textAnchor = GetComponent<Text>();
            textAnchor.text = DEFAULT_MESSAGE;
        }
    }
}
