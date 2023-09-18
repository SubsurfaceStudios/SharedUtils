using System;
using UnityEngine;

namespace SubsurfaceStudios.Rendering {
    public static class DirectRender {
        public static readonly Mesh QuadIdentity = new Mesh() {
            vertices = new Vector3[] {
                new(0, 0, 0),
                new(1, 0, 0),
                new(0, 1, 0),
                new(1, 1, 0),
            },
            triangles = new int[] {
                0, 2, 1,
                2, 3, 1,
            },
            uv = new Vector2[] {
                new(0, 0),
                new(1, 0),
                new(0, 1),
                new(1, 1),
            },
        };

        public static readonly Material Unlit = new Material(Shader.Find("Unlit/Transparent"));

        public static float GetAspectRatio(this RenderTexture target) {
            return (float)target.width / (float)target.height;
        }

        public static DirectRenderContext Enter2DRenderingContext(this RenderTexture target) {
            return new DirectRenderContext(
                Matrix4x4.Ortho(
                    // Rect
                    0, 1, 0, 1, 
                    
                    // Near plane
                    -100,
                    
                    // Far plane
                    100
                ),
                target
            );
        }
    }

	public class DirectRenderContext : IDisposable {
        public RenderTexture old_rt;

        public DirectRenderContext(Matrix4x4 matrix, RenderTexture target) {
            old_rt = RenderTexture.active;
            RenderTexture.active = target;

            GL.PushMatrix();
            GL.LoadProjectionMatrix(matrix);
        }

		public void Dispose() {
            GL.PopMatrix();
            RenderTexture.active = old_rt;
		}

        public void DrawRect(Color color, Rect quad) {
            DrawQuad(Texture2D.whiteTexture, color, quad);
        }

        public void Clear(Color color) {
            GL.Clear(true, true, color);
        }

        public void DrawQuad(Texture tex, Color tint, Rect quad) {
            Matrix4x4 transform = Matrix4x4.TRS(
                quad.position,
                Quaternion.identity,
                quad.size
            );

            if (!DirectRender.Unlit.SetPass(0))
                Debug.LogError("SetPass returned false");

            DirectRender.Unlit.mainTexture = tex;
            DirectRender.Unlit.color = tint;

            Graphics.DrawMeshNow(DirectRender.QuadIdentity, transform);
        }
	}
}
