using UnityEngine;

using Gameflakes.TurtleType.GameController;

namespace Gameflakes.InputSetup
{
    public sealed class InputManager : MonoBehaviour, IInputConfiguration
    {
        // GM communication
        private static GameSystemManager gsm;
        private static readonly GameSystemManager.typeOfMessage gameInputMessage = GameSystemManager.typeOfMessage.RecieveGameInput;
        // private static readonly GameSystemManager.typeOfMessage uiInputMessage = GameSystemManager.typeOfMessage.RecieveUIInput;
        private static readonly GameSystemManager.typeOfMessage awakeMessage = GameSystemManager.typeOfMessage.DefaultKeys;

        // Input configuration
        private char lastInput;
        private char[ ] validKeys;

        public char GetLastInput ( )
        {
            return lastInput;
        }

        public void SetValidKeys ( char[ ] keys )
        {
            if ( keys == null || keys.Length == 0 )
            {
                throw new System.ArgumentOutOfRangeException ( );
            }

            validKeys = new char [ keys.Length ];
            for ( int i = 0; i < keys.Length; i++ )
            {
                validKeys[ i ] = keys[ i ];
            }

        }

        private void Awake ( )
        {
            gsm = gameObject.GetComponent<GameSystemManager> ( );
            AlertGM ( awakeMessage );
        }

        private void Update ( )
        {
            if ( Input.anyKeyDown )
            {
                char keyPressed;
                try
                {
                    keyPressed = GetValidKeyInput();
                }
                catch ( System.AccessViolationException ) { return; }

                InputAlertToGM ( keyPressed );
            }
        }

        private char GetValidKeyInput ( )
        {
            foreach ( char key in validKeys )
            {
                if ( Input.GetKeyDown ( CharToKeyCode ( key ) ) )
                {
                    return key;
                }
            }

            throw new System.AccessViolationException ( );
        }

        private KeyCode CharToKeyCode ( char key )
        {
            return ( KeyCode ) System.Enum.Parse ( typeof ( KeyCode ), ( key + "" ) );
        }

        // Alert and GM decides what to do.
        private void InputAlertToGM ( char keyPressed )
        {
            lastInput = char.ToLower ( keyPressed );
            AlertGM ( gameInputMessage );
        }

        private void AlertGM ( GameSystemManager.typeOfMessage messageType )
        {
            gsm.Alert ( messageType );
        }
    }
}

