
namespace Gameflakes.TurtleType.LevelController
{
    public static class LevelCharacteristics
    {
        public static readonly int NUMBER_OF_LEVELS = 6;
        private static readonly LevelConfig[ ] LEVELS_CONFIG = new LevelConfig[ ]
                                                                        {
                                                                            new LevelConfig(10, 4, 5.0f),
                                                                            new LevelConfig(10, 5, 4.0f),
                                                                            new LevelConfig(10, 6, 3.0f),
                                                                            new LevelConfig(10, 7, 3.0f),
                                                                            new LevelConfig(10, 7, 2.0f),
                                                                            new LevelConfig(10, 8, 2.0f)
                                                                        };

        public static int GetLevelNumberOfWords ( int level )
        {
            return LEVELS_CONFIG[ level ].GetLevelNumberOfWords ( );
        }

        public static int GetLevelNumberOfLetters ( int level )
        {
            return LEVELS_CONFIG[ level ].GetLevelNumberOfLetters ( );
        }

        public static float GetLevelTime ( int level )
        {
            return LEVELS_CONFIG[ level ].GetLevelTime ( );
        }

        struct LevelConfig
        {
            private int numberOfWords;
            private int numberOfLetters;
            private float time;

            public LevelConfig ( int numberOfWords, int numberOfLetters, float time )
            {
                this.numberOfWords = numberOfWords;
                this.numberOfLetters = numberOfLetters;
                this.time = time;
            }

            public int GetLevelNumberOfWords()
            {
                return numberOfWords;
            }

            public int GetLevelNumberOfLetters()
            {
                return numberOfLetters;
            }

            public float GetLevelTime ( )
            {
                return time;
            }
        }
    }
}