using UnityEngine;

namespace LifeTown
{
    public static class GamePolicy
    {
        // Default Volume Settings
        public const float DEFAULT_BGM_VOLUME = 0.8f;
        public const float DEFAULT_SFX_VOLUME = 0.8f;

        /*
         * Volume Setting Policy:
         * - User's volume settings should be saved using PlayerPrefs.
         * - PlayerPrefs keys: "User_BGM_Volume", "User_SFX_Volume".
         * - If no PlayerPrefs value exists, fallback to the default volume constants defined above.
         * - Values should be clamped between 0.0f and 1.0f.
         */
    }
}
