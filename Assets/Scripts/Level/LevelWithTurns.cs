using System.Collections;
using UnityEngine;

namespace Gameflakes.TurtleType.LevelController
{
    public sealed class LevelWithTurns
    {
        private Turn[ ] turns;
        private int indexCurrentTurn;

        private float time;
        private float currentTurnTimeLeft;

        public LevelWithTurns ( string[ ] words, int numberOfTurns, float time )
        {
            if ( numberOfTurns <= 0 )
            {
                throw new System.ArgumentOutOfRangeException ( numberOfTurns + " is out of range." );
            }

            if ( time <= 0.0f )
            {
                throw new System.ArgumentOutOfRangeException ( time + " is out of range." );
            }

            turns = new Turn[ numberOfTurns ];

            for ( int i = 0; i < numberOfTurns; i++ )
            {
                turns[ i ] = new Turn ( words[ i ] );
            }
            
            indexCurrentTurn = 0;
            this.time = time;
            currentTurnTimeLeft = time;
        }

        /// <summary>
        /// It's used to check if the char given is valid and equals to the word's next char.
        /// (Check Turn's NextChar method for more information).
        /// </summary>
        /// 
        /// <param name="c">
        /// It's a character that is given to be compared with the word's next char.
        /// </param>
        /// 
        /// <returns>
        /// Returns false if char is not valid or false;
        /// Returns true if char is equals to the word's next char.
        /// </returns>
        /// 
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException if the char given as a parameter is not valid.
        /// </exception>
        public bool TypedNextChar ( char c )
        {
            int result = -1;

            try
            {
                result = turns[ indexCurrentTurn ].NextChar ( c );
            }
            catch ( System.ArgumentOutOfRangeException ) { };

            return result == -1 ? false : true;
        }

        /// <summary>
        /// This method check the state of the turn by doing these steps:
        /// If there are no more Turns left, the Level is over.
        /// If the turn was won, current turn is changed to the next turn,
        /// but if the turn was lost, the current turn is going to be restarted.
        /// </summary>
        /// 
        /// <returns>
        /// Returns 1 if the current turn became the next one;
        /// Returns 0 if the current turn was lost;
        /// Returns -1 if the Level has no more turns.
        /// </returns>
        /// 
        /// <exception cref="System.AccessViolationException">
        /// Throws AccessViolationException if trying to go to the next turn, without finishing it first.
        /// </exception>
        public int GoToNextTurnIfNotEnd ( )
        {
            if ( IsTurnTimeOver ( ) )
            {
                turns[ indexCurrentTurn ].ResetTurn ( );
                currentTurnTimeLeft = time;
                return 0;
            }

            if ( !turns[ indexCurrentTurn ].TurnIsOver ( ) )
            {
                UpdateCurrentTurnTime ( );
                throw new System.AccessViolationException ( "The turn hasn't finished yet." );
            }

            if ( AllTurnsCompleted ( ) )
            {
                return -1;
            }

            indexCurrentTurn++;
            currentTurnTimeLeft = time;

            return 1;
        }

        public bool IsTurnTimeOver ( )
        {
            return currentTurnTimeLeft <= 0.0f;
        }

        private void UpdateCurrentTurnTime ( )
        {
            currentTurnTimeLeft -= Time.deltaTime;
        }

        /// <summary>
        /// It's used to check if all turns of the current level have been finished.
        /// </summary>
        /// 
        /// <returns>
        /// Returns false if any turn's been not finished.
        /// Returns true if all turns've been finished.
        /// </returns>
        private bool AllTurnsCompleted ( )
        {
            foreach ( Turn t in turns )
            {
                if ( !t.TurnIsOver ( ) )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// It's used to encapsule the concept of Turn.
        /// (See the constructor for more info.)
        /// </summary>
        public struct Turn
        {
            // Final variables
            private string text;
            
            // Dynamic variables
            private int indexNextChar;
            private bool turnFinished;
            private bool turnWon;

            public Turn ( string text )
            {
                this.text = text;

                indexNextChar = 0;
                turnFinished = false;
                turnWon = false;
            }

            /// <summary>
            /// It's used to check if the turn's completed or failed.
            /// </summary>
            /// 
            /// <returns>
            /// Returns true if the turn's ended for any reason.
            /// </returns>
            public bool TurnIsOver ( )
            {
                return turnFinished;
            }

            /// <summary>
            /// It's used to check if the turn was won.
            /// </summary>
            /// 
            /// <returns>
            /// Returns true if turn was won, else returns false;
            /// </returns>
            /// 
            /// <exception cref="System.AccessViolationException">
            /// Throws AccessViolationException when the turn isn't over yet.
            /// </exception>
            public bool TurnWon ( )
            {
                if ( !TurnIsOver ( ) )
                {
                    throw new System.AccessViolationException ( "Turn is not over yet." );
                }

                return turnWon;
            }

            /// <summary>
            /// It's used to reset the turn,
            /// as if it had begun right now.
            /// (Reset all variables to their initial value)
            /// </summary>
            public void ResetTurn ( )
            {
                turnWon = false;
                turnFinished = false;
                indexNextChar = 0;
            }

            /// <summary>
            /// It's used to compare two characters:
            ///  - the paramater;
            ///  - the Turn's nextChar.
            /// If they're equal, get the next character of the text
            /// and put it on the nextChar variable, as well as return the number of total characters it went by already.
            /// If the're not equal, returns -1 and don't execute the change described above.
            /// </summary>
            /// 
            /// <param name="c">
            /// It's a character given to compare with the nextChar.
            /// </param>
            /// 
            /// <returns>
            /// Returns the number of letters it went by already;
            /// If the character given doesn't match the next character, returns -1.
            /// </returns>
            /// 
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// Throws ArgumentOutOfRangeException if the Turn has already been completed.
            /// </exception>
            public int NextChar ( char c )
            {
                if ( indexNextChar >= text.Length )
                {
                    throw new System.ArgumentOutOfRangeException ( indexNextChar + " is out of range." );
                }

                if ( c == text[ indexNextChar ] )
                {
                    if ( indexNextChar == ( text.Length - 1 ) )
                    {
                        turnFinished = true;
                        turnWon = true;
                        indexNextChar = text.Length;
                        return indexNextChar;
                    }

                    indexNextChar++;
                    return indexNextChar;
                }

                return -1;
            }
        }
    }
}
