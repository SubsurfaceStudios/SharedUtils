using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

#if UNITY_EDITOR
    [MenuItem("GameObject/3D Object/Event Volume", false, 10)]
    static void CreateEventVolume(MenuCommand command) {
        GameObject go = new("Event Volume");
        GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);

        Undo.RegisterCreatedObjectUndo(go, $"Create {go.name}");
        Selection.activeObject = go;

        Collider c = go.AddComponent<BoxCollider>();
        c.isTrigger = true;
        go.AddComponent<EventVolume>();
    }
#endif
    }
}
