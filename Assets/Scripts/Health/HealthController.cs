
namespace Gameflakes.HealthSystem
{
    public sealed class HealthController : IHealthSystem
    {
        private HealthController ( ) { }
        private static readonly IHealthSystem singletonInstance = new HealthController ( );
        public static IHealthSystem GetSingletonInstance ( )
        {
            return singletonInstance;
        }

        private readonly static int DEFAULT_HEALTH = 3;
        private int currentHealth;

        public void InitializeHealth ( )
        {
            currentHealth = DEFAULT_HEALTH;
        }

        public void TakeDamage ( int damage )
        {
            currentHealth -= damage;
        }

        public void HealDamage ( int healing )
        {
            currentHealth += healing;
        }

        public bool IsItDead ( )
        {
            return currentHealth <= 0;
        }

        public void ResetStatus ( )
        {
            currentHealth = DEFAULT_HEALTH;
        }

        public int GetHealth ( )
        {
            return currentHealth;
        }
    }
}
