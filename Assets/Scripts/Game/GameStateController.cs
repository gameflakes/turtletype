
namespace Gameflakes.TurtleType.GameController
{
    public sealed class GameStateController : IGameStates
    {
        private static EGameState currentState;
        private static readonly EGameState INITIAL_GAME_STATE = EGameState.MenuActive;
        private static readonly EGameState TEMPORARY_INITIAL_GAME_STATE = EGameState.Playing;

        // Singleton
        private GameStateController ( ) { }
        private static readonly IGameStates singletonInstance = new GameStateController ( );
        public static IGameStates GetSingletonInstance ( )
        {
            return singletonInstance;
        }

        // Has temporary variable
        void Awake ( )
        {
            currentState = TEMPORARY_INITIAL_GAME_STATE;
        }

        public bool isMenuActive ( )
        {
            return currentState == EGameState.MenuActive;
        }

        public bool isLoading ( )
        {
            return currentState == EGameState.Loading;
        }

        public bool isPlaying ( )
        {
            return currentState == EGameState.Playing;
        }

        public bool isPausing ( )
        {
            return currentState == EGameState.Pausing;
        }

        public EGameState QuitGame ( )
        {
            if ( currentState != EGameState.Pausing ||
                 currentState != EGameState.Playing )
            {
                throw new System.InvalidOperationException();
            }

            EGameState holder = currentState;
            currentState = EGameState.MenuActive;

            return holder;
        }

        public EGameState LoadLevel ( )
        {
            if (currentState != EGameState.Playing)
            {
                throw new System.InvalidOperationException();
            }

            EGameState holder = currentState;
            currentState = EGameState.Loading;

            return holder;
        }

        public EGameState PauseGame ( )
        {
            if ( currentState != EGameState.Playing )
            {
                throw new System.InvalidOperationException ( );
            }

            EGameState holder = currentState;
            currentState = EGameState.Pausing;

            return holder; 
        }

        public EGameState ResumeGame ( )
        {
            if (currentState != EGameState.Pausing)
            {
                throw new System.InvalidOperationException();
            }

            EGameState holder = currentState;
            currentState = EGameState.Playing;

            return holder;
        }
    }
}
