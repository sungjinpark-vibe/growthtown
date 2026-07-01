using UnityEngine;

namespace LifeTown.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayBGM(string trackName)
        {
            Debug.Log($"[AudioManager] Playing BGM with crossfade: {trackName}");
        }

        public void PlaySFX(string sfxName)
        {
            Debug.Log($"[AudioManager] Playing SFX (Pooled): {sfxName}");
        }
    }
}