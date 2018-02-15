using UnityEngine;

using Gameflakes.TurtleType.FileController;
using Gameflakes.TurtleType.TextModifications;
using Gameflakes.TurtleType.LevelController;

using Gameflakes.HealthSystem;
using Gameflakes.InputSetup;

namespace Gameflakes.TurtleType.GameController
{
    public sealed class GameSystemManager : MonoBehaviour
    {
        /// <summary>
        /// This enum stores the possible messages classes can send to the GameSystemManager.
        /// 
        /// If a new message is added here, it needs to be implemented on the Alert method too.
        /// </summary>
        public enum typeOfMessage {
                                    RecieveUIInput, RecieveGameInput, DefaultKeys, LoadLevel,
                                    LetterMatch, TextFailed, TextSuceed, LevelSuceed
                                  };

        // Dependencies to interfaces
        IGameStates gameStateController;
        IWordFileReader wordFileReader;
        IInputConfiguration inputManager;
        IHealthSystem healthController;

        // Dependencies to classes directly
        int levelCounter = 0;
        LevelManager levelManager;
        TextManager textManager;

        private void Awake ( )
        {
            gameStateController = GameStateController.GetSingletonInstance ( );
            wordFileReader = WordFileController.GetSingletonInstance ( );
            healthController = HealthController.GetSingletonInstance ( );
            healthController.InitializeHealth ( );

            textManager = GameObject.Find ( "TextAnchor" ).GetComponent<TextManager> ( );
            textManager.HealthUpdate ( healthController.GetHealth ( ) );

            inputManager = gameObject.GetComponent<InputManager> ( );
            levelManager = gameObject.GetComponent<LevelManager> ( );
        }

        // Implement Main Loop
        private void Update ( )
        {
            // Send new time to UI
            if ( levelCounter >= 1 )
            {
                textManager.UpdateTime ( levelManager.GetCurrentTime ( ) );
                return;
            }

            // First level creation
            LoadLevel ( );
        }

        // Load level and all it's dependencies.
        private void LoadLevel ( )
        {
            int numLetters = LevelCharacteristics.GetLevelNumberOfLetters ( levelCounter );
            int numWords = LevelCharacteristics.GetLevelNumberOfWords ( levelCounter );
            float time = LevelCharacteristics.GetLevelTime ( levelCounter );
            string[] words = wordFileReader.ReadSpecificFileAndGetRandomWords ( LevelCharacteristics.GetLevelNumberOfWords ( levelCounter ), 
                                                                                LevelCharacteristics.GetLevelNumberOfLetters ( levelCounter ) );
            levelManager.InitializeLevel ( words, numWords, numLetters, time );
            textManager.SetLevelWords ( words );
            textManager.UpdateShowingWord ( );

            levelCounter++;
        }

        /// <summary>
        /// It's used to recieve specific messages,
        /// as a way of communication between different aspects of the game.
        /// 
        /// It needs to implement a method for each message defined on the enum above.
        /// </summary>
        /// 
        /// <param name="message">
        /// It's a enum type, that represents the message being recieved.
        /// </param>
        public void Alert ( typeOfMessage message )
        {
            switch ( message )
            {
                case typeOfMessage.RecieveUIInput:
                    RecieveUIInput ( );
                    return;
                case typeOfMessage.RecieveGameInput:
                    RecieveGameInput ( );                        
                    return;
                case typeOfMessage.DefaultKeys:
                    DefaultKeys ( );
                    return;
                case typeOfMessage.LetterMatch:
                    LetterMatch ( );
                    return;
                case typeOfMessage.TextFailed:
                    TextFailed ( );
                    return;
                case typeOfMessage.TextSuceed:
                    TextSuceed ( );
                    return;
                case typeOfMessage.LevelSuceed:
                    LevelSuceed ( );
                    return;
            }
        }

        // implement this after UI
        private void RecieveUIInput ( )
        {
            
        }

        // Called when InputManager detect's an input.
        private void RecieveGameInput ( )
        {
            levelManager.ManageInput ( inputManager.GetLastInput ( ) );
        }

        // Called when InputManager is initialized (it's Awake() function).
        private void DefaultKeys ( )
        {
            /* original:
             * if ( gameStateController.isMenuActive ( ) )
             * 
             * temporary:
             */
            inputManager.SetValidKeys ( InputConfiguration.GetValidKeys ( ) );
        }

        // Update on UI
        private void LetterMatch ( )
        {
            // do something on UI
            textManager.GotNextLetterRight ( );
        }

        // Update on UI and Health
        private void TextFailed ( )
        {
            // do something on UI and on health system as well.

            healthController.TakeDamage ( 1 );
            textManager.HealthUpdate ( healthController.GetHealth ( ) );
            if ( healthController.IsItDead ( ) )
            {
                textManager.LostGameMessage ( );
                inputManager.DeactivateOrActivateInput ( );
                return;
            }

            textManager.ResetWord();
        }

        // Update on UI and, maybe, Health
        private void TextSuceed ( )
        {
            // do something on UI
            textManager.UpdateShowingWord ( );
        }

        // Update on UI and, maybe, Health
        private void LevelSuceed ( )
        {
            // do something on UI and, maybe, on health system as well.

            if ( levelCounter >= LevelCharacteristics.NUMBER_OF_LEVELS )
            {
                // Won the game
                textManager.FinishGameMessage ( );
                return;
            }

            LoadLevel ( );
            healthController.HealDamage ( 1 );
            textManager.HealthUpdate ( healthController.GetHealth ( ) );
        }
    }
}
