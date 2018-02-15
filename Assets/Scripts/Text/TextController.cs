using UnityEngine;
using UnityEngine.UI;

namespace Gameflakes.TextMechanics
{
    public sealed class TextController : MonoBehaviour, ITextHolder
    {
        public Text textObject;

        public string UpdateTextContent ( string newText )
        {
            if ( newText == null )
            {
                throw new System.ArgumentNullException ( );
            }

            string holder = textObject.text;

            if ( newText == "" )
            {
                textObject.text = "";
                return holder;
            }

            textObject.text = string.Copy ( newText );
            return holder;
        }

        public string ClearTextContent ( )
        {
            return UpdateTextContent ( "" );
        }

        public string GetTextContent ( )
        {
            return string.Copy ( textObject.text );
        }
    }
}
