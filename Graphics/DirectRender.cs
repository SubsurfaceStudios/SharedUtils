using System;
using UnityEngine;

namespace SubsurfaceStudios.Graphics {
    public static class DirectRender {
        public static RenderTexture Now(Vector2 size, Matrix4x4 matrix, Action<RenderTexture> perform_actions) {
            RenderTexture old = RenderTexture.active;

            RenderTexture rt = new((int)size.x, (int)size.y, 0);
            if (!rt.Create()) {
                rt.Release();
                return null;
            };

            RenderTexture.active = rt;
            GL.PushMatrix();
            GL.LoadProjectionMatrix(matrix);

            try {
                perform_actions(rt);
            } finally {
                // Clean up after ourselves.
                GL.PopMatrix();
                GL.invertCulling = false;
                RenderTexture.active = old;
            }

            return rt;
        }
    }
}
