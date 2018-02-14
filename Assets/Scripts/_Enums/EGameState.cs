
namespace Gameflakes.TurtleType.GameController
{
    /// <summary>
    /// This enum describe all possible game states of Turtle Type.
    /// 
    /// If a new game state is added to this,
    /// it's needed to create the game state's methods on the GameState interface,
    /// as well as implement it's intelligence on the interface's implementation.
    /// 
    /// The enum name needs to be self sufficient to define itself;
    /// It's recommended to use a verb + 'ing' (Loading)
    ///                      or a name + it's state (MenuActive).
    /// </summary>
    public enum EGameState
    {
        MenuActive, Loading, Playing, Pausing
    }
}
