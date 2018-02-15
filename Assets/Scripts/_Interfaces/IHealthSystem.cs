
namespace Gameflakes.HealthSystem
{
    /* Supposedly we need to document the interfaces properly,
     * but I'm not on the mood to do it :x
     */
    public interface IHealthSystem
    {
        // Decrease x health, if possible. (x being the value of damage)  
        void TakeDamage ( int damage );
        
        // Increase x health, if possible. (x being the value of healing)
        void HealDamage ( int healing );
        
        /* Returns true if the health is 0 or below,
         * on contrary, returns false.
         */
        bool IsItDead ( );

        // Reset health to it's default value.
        void ResetStatus ( );

        // Returns current health value.
        int GetHealth ( );

        // Define a value to the current health.
        void InitializeHealth();
    }
}
