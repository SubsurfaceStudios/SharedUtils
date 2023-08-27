# Bytes
---
#### Type: `public static class`  
#### Namespace: `SubsurfaceStudios.Utilities.Memory.Unsafe`  

## Summary
---
Provides utilities for manipulating data structures in memory.
Also usable for things like protocol implementations and serialization.

## Static Methods
---

### Of\<T\>()
#### Type: `public static byte[]`  
#### Returns: `byte[]` (potentially null)  
#### Type Arguments:
- `T` (where `T: struct`): The type of structure.
#### Arguments:  
- `in T value`: The structure to convert to a byte array.

Example:  
```csharp
using SubsurfaceStudios.Utilities.Memory.Unsafe;
using System.Runtime.InteropServices;
using UnityEngine;

public class ExampleBehaviour : MonoBehaviour {
	// This is purely an example - there's not much reason to do this
	// in practice.
	[StructLayout(LayoutKind.Explicit)]
	public struct ExampleStruct {
		[FieldOffset(0)]
		public byte A;
	}

	// Start is called before the first frame update.
	void Start() {
		// While ordinarily this would probably be changed by the
		// CLR, we manually construct a structure that takes up
		// exactly one byte, which we control.
		ExampleStruct example_struct = new() {
			A = 0xEF
		};

		// We attempt to get the memory representation of the
		// structure. If this fails, we get an exception.
		byte[] bytes = Bytes.Of(example_struct);
		// Make sure everything went as planned.
		// Check our assumptions about how the struct is laid out in memory.
		Debug.Assert(bytes.Length == 1);
		Debug.Assert(bytes[0] == 0xEF);

		Debug.Log("Memory representation of structure \"ExampleStruct\" is exactly 1 byte with the value 0xEF.");
	}
}
```

---

### To\<T\>()
#### Type: `public static T?`
#### Returns: `T?` (potentially null)
#### Type Arguments:
- `T` (where `T: struct`): The type of structure.
#### Arguments:
- `this byte[] bytes`: The byte array to convert to a structure.

Example:
```csharp
using SubsurfaceStudios.Utilities.Memory.Unsafe;
using System.Runtime.InteropServices;
using UnityEngine;

public class ExampleBehaviour : MonoBehaviour {
	// This is purely an example - there's not much reason to do this
	// in practice.
	[StructLayout(LayoutKind.Explicit)]
	public struct ExampleStruct {
		[FieldOffset(0)]
		public byte A;
	}

	// Start is called before the first frame update.
	void Start() {
		// Manually create the struct in memory.
		byte[] bytes = new byte[1] { 0xEF };

		// Convert the bytes into a struct.
		ExampleStruct? example = bytes.To<ExampleStruct>();

		// Confirm everything works as intended
		Debug.Assert(example.HasValue, "Conversion failed!");
		Debug.Assert(example.Value.A == 0xEF, "The value of the struct does not match what we entered.");

		Debug.Log("Conversion successful!");
	}
}
```
