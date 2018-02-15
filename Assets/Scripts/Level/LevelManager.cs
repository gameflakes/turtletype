using UnityEngine;

using Gameflakes.TurtleType.GameController;

namespace Gameflakes.TurtleType.LevelController
{
    public class LevelManager : MonoBehaviour
    {
        // GM communication
        private static GameSystemManager gsm;
        private static readonly GameSystemManager.typeOfMessage textFailed = GameSystemManager.typeOfMessage.TextFailed;
        private static readonly GameSystemManager.typeOfMessage textSuceed = GameSystemManager.typeOfMessage.TextSuceed;
        private static readonly GameSystemManager.typeOfMessage levelSuceed = GameSystemManager.typeOfMessage.LevelSuceed;
        private static readonly GameSystemManager.typeOfMessage letterMatch = GameSystemManager.typeOfMessage.LetterMatch;

        // Level variables
        private LevelWithTurns currentLevel;
        private int numberOfLettersForWord;
        private int numberOfRightLettersTyped;
        private bool levelHasBegun;

        private void Awake ( )
        {
            gsm = gameObject.GetComponent<GameSystemManager> ( );
        }

        public void InitializeLevel ( string[] words, int numberOfTurns, int numberOfLetters, float time )
        {
            currentLevel = new LevelWithTurns ( words, numberOfTurns, time );
            levelHasBegun = true;
            numberOfLettersForWord = numberOfLetters;
            numberOfRightLettersTyped = 0;
        }

        public void ManageInput ( char c  )
        {
            if ( !levelHasBegun )
            {
                throw new System.AccessViolationException ( "The level isn't initialized yet." );
            }

            // Update will resolve it
            if ( numberOfRightLettersTyped >= numberOfLettersForWord )
            {
                return;
            }

            if ( currentLevel.TypedNextChar ( c ) )
            {
                numberOfRightLettersTyped++;
                AlertGM ( letterMatch );
            }
        }

        public float GetCurrentTime ( )
        {
            return currentLevel.GetCurrentTime ( );
        }

        private void Update ( )
        {
            if ( !levelHasBegun )
            {
                return;
            }

            // -- milestone: level has been initiated.
            try
            {
                int next = currentLevel.GoToNextTurnIfNotEnd ( );
                
                if ( next == 1 )
                {
                    numberOfRightLettersTyped = 0;
                    AlertGM ( textSuceed );
                    return;
                }

                if ( next == 0 )
                {
                    numberOfRightLettersTyped = 0;
                    AlertGM ( textFailed );
                    return;
                }

                if ( next == -1 )
                {
                    levelHasBegun = false;
                    AlertGM ( levelSuceed );
                    return;
                }
            }
            catch ( System.AccessViolationException ) { }

            // -- milestone: turn isn't over yet.
        }

        private void AlertGM (GameSystemManager.typeOfMessage message)
        {
            gsm.Alert ( message );
        }
    }
}
