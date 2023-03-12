using UnityEngine;

namespace SubsurfaceStudios.Audio {
    public class Audio : MonoBehaviour {
        [SerializeField]
        private AudioSource m_Source;

        private static Audio s_Instance;

        void Awake() {
            s_Instance = this;
        }

        public static void Play2D(AudioClip clip) {
            s_Instance.m_Source.PlayOneShot(clip);
        }
    }
}
