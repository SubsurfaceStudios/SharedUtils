using System;
using UnityEngine;

namespace SubsurfaceStudios.Audio {
    public class Audio : MonoBehaviour {
        [SerializeField]
        private AudioSource m_Source;

        private static Audio s_Instance;

        void OnEnable() {
            s_Instance = this;
        }

        public static void Play2D(AudioClip clip) {
            s_Instance.m_Source.PlayOneShot(clip);
        }

        public static AudioHandle Handle(AudioClip clip) {
            return new AudioHandle(clip);
        }
    }

    public sealed class AudioHandle : IDisposable {
        private AudioSource m_Source;

		private bool m_LeakAudioSource = false;

		internal AudioHandle(AudioClip clip) {
            AudioSource source = new GameObject("Managed Audio").AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = clip;
            source.spatialBlend = 0.0f;
            m_Source = source;
        }

        public AudioSource Leak() {
            m_LeakAudioSource = true;
            return m_Source;
        }

        public AudioSource Source => m_Source;

        public void Play() => m_Source.Play();
		public void Stop() => m_Source.Stop();
		public void Pause() => m_Source.Pause();
		public void UnPause() => m_Source.UnPause();

        public bool IsPlaying => m_Source.isPlaying;
        public AudioClip Clip {
            get => m_Source.clip;
            set => m_Source.clip = value;
        }

        public void Seek(float time) => m_Source.time = time;

        public float Time {
			get => m_Source.time;
			set => m_Source.time = value;
		}

		~AudioHandle() {
            Dispose();
        }

        public void Dispose() {
            try {
                if (m_Source != null && !m_LeakAudioSource) {
                    GameObject.Destroy(m_Source.gameObject);
                }
            } finally {
                m_Source = null;
            }
        }
    }
}
