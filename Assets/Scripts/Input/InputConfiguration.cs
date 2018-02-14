using UnityEngine;

namespace Gameflakes.InputSetup
{
    public static class InputConfiguration
    {
        private static KeyCode[ ] inputKeys = new KeyCode[ ] {
                                                KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
                                                KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
                                                KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
                                                KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
                                                KeyCode.Y, KeyCode.Z, KeyCode.Escape
                                            };

        public static char[ ] GetValidKeys ( )
        {
            if ( inputKeys == null || inputKeys.Length == 0 )
            {
                throw new System.AccessViolationException ( );
            }

            char[ ] keys = new char[ inputKeys.Length ];
            for ( int i = 0; i < inputKeys.Length; i++ )
            {
                keys[ i ] = KeyCodeToChar ( inputKeys[ i ] );
            }

            return keys;
        }

        private static char KeyCodeToChar ( KeyCode keyCode )
        {
            return keyCode.ToString ( )[ 0 ];
        }

        private static KeyCode CharToKeyCode ( char key )
        {
            return ( KeyCode ) System.Enum.Parse ( typeof ( KeyCode ), ( key + "" ) );
        }
    }
}
