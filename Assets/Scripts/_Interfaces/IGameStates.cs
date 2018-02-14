
namespace Gameflakes.TurtleType.GameController
{
    /// <summary>
    /// The methods described here cover all the enum's Game States.
    /// 
    /// For each game state defined on enum, there are two methods defined:
    ///     - is + the enum name (isMenuActive, isLoading);
    ///         It returns true if the method's enum is equals to the a variable which controls the current state.
    ///     - enum action (PauseGame, LoadLevel)
    ///         It returns the older enum and change the variable defining the current state to the method's enum.
    /// </summary>
    public interface IGameStates
    {
        bool isMenuActive ( );
        bool isLoading ( );
        bool isPlaying ( );
        bool isPausing ( );

        EGameState QuitGame ( );
        EGameState LoadLevel ( );
        EGameState PauseGame ( );
        EGameState ResumeGame ( );
    }
}
