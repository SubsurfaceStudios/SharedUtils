using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SubsurfaceStudios {
    public sealed class EventVolume : MonoBehaviour {
        public LayerMask m_PlayerLayerMask;

        public UnityEvent m_OnPlayerEnter;
        public UnityEvent m_OnPlayerExit;

        public LayerMask m_ObjectLayerMask;

        public UnityEvent m_OnObjectEnter;
        public UnityEvent m_OnObjectExit;

        void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & m_PlayerLayerMask.value) > 0) {
                m_OnPlayerEnter.Invoke();
            } else if (((1 << other.gameObject.layer) & m_ObjectLayerMask.value) > 0) {
                m_OnObjectEnter.Invoke();
            }
        }

        void OnTriggerExit(Collider other) {
            if (((1 << other.gameObject.layer) & m_PlayerLayerMask.value) > 0) {
                m_OnPlayerExit.Invoke();
            } else if (((1 << other.gameObject.layer) & m_ObjectLayerMask.value) > 0) {
                m_OnObjectExit.Invoke();
            }
        }
    }
}
