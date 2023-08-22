using System.Collections;
using UnityEngine;

namespace SubsurfaceStudios.Utilities.Async {

    public class StaticCoroutineHandler : MonoBehaviour
    {
        private static StaticCoroutineHandler Instance;

        void Awake() {
            Instance = this;
            DontDestroyOnLoad(transform.root);
        }
        public static Coroutine StartCoroutineStatic(IEnumerator routine) => Instance.StartCoroutine(routine);
        public static void StopCoroutineStatic(Coroutine routine) => Instance.StopCoroutine(routine);
        public static void StopCoroutineStatic(IEnumerator routine) => Instance.StopCoroutine(routine);
        public static void StopCoroutineStatic(string routine_name) => Instance.StopCoroutine(routine_name);
        public static void StopAllCoroutinesStatic() => Instance.StopAllCoroutines();

    }
}
