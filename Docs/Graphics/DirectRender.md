# [SharedUtils](../README.md)/[Graphics](./README.md)/DirectRender
---
#### Type: `public static class`  
#### Namespace: `SubsurfaceStudios.Graphics`  

## Summary
---
This provides a simple implementation of a rendering API that is used for writing directly to a `RenderTexture`.

## Static Methods
---

### Now()
#### Type: `public static RenderTexture`  
#### Returns: `RenderTexture`  
#### Arguments:  
- `Vector2Int size`: The size (in pixels) of the render texture to create.
- `Matrix4x4 matrix`: The projection matrix to use while executing the specified actions.
- `Action<RenderTexture> perform_actions`: The actions to execute while rendering to the render texture.

#### Example:  
```csharp
using SubsurfaceStudios.Graphics;
using UnityEngine;

public class ExampleBehaviour : MonoBehaviour {
	// The material we will use for rendering.
	public Material Material;
	// The renderer we will apply the material & texture to.
	public Renderer Renderer;
	// The texture we will blit to the render texture.
	public Texture2D BlitSource;

	// Start is called before the first frame update.
	void Start() {
		// Apply the material to the renderer.
		Renderer.material = Material;
		
		// Get the rendering context we need.
		RenderTexture tex = DirectRender.Now(
			// The size of the render texture - in this case
			// we're using the size of the source texture.
			new Vector2Int(BlitSource.width, BlitSource.height),
			// The projection matrix to use. In this case, we're
			// not rendering anything, so we can just use a basic
			// orthographic projection.
			Matrix4x4.Ortho(0, 1, 1, 0, -1, 1),
			// What do we want to run while the render context is
			// active?
			(RenderTexture mutable) => {
				// Blit the source texture to the entirety of the render
				// texture. This will have the effect of setting the entire
				// render texture equal to the source texture.
				Graphics.Blit(BlitSource, mutable);
			}
		);

		// Apply the render texture to our material. It will then display
		// on the renderer we set.
		Material.mainTexture = tex;
	}
}
```
