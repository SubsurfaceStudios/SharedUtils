using UnityEngine;

namespace SubsurfaceStudios.Utilities.Constants {
    public static class UnityConstants {
        private static Camera mainCamera;
        public static Camera MainCamera {
            get {
                if(mainCamera == null)
                    mainCamera = Camera.main;
                return mainCamera;
            }
        }
    }
}