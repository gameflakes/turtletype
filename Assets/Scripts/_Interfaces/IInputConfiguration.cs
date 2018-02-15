
namespace Gameflakes.InputSetup
{
    public interface IInputConfiguration
    {
        /// <summary>
        /// It's used to get the last input recieved by the name of the key pressed down.
        /// </summary>
        /// 
        /// <returns>
        /// Returns a string copy of the last input recieved.
        /// </returns>
        /// 
        /// <exception cref="System.AccessViolationException">
        /// Throws AccessViolationException if the last input was not recieved.
        /// </exception>
        char GetLastInput ( );

        /// <summary>
        /// It's used to set the names of all valid keys,
        /// so the Input can be validated as valid or invalid.
        /// </summary>
        /// 
        /// <param name="keys">
        /// It's a string array that has the names of all valid keys.
        /// </param>
        /// 
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException if the array of strings given is null or empty.
        /// </exception>
        void SetValidKeys ( char[ ] keys );

        // Change the script active state.
        void DeactivateOrActivateInput ( );
    }
}
