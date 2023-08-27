# [SharedUtils](../README.md)/[Memory](./README.md)/OverallocatingArray<T>
---
#### Type:
`public class`  
#### Namespace:
`SubsurfaceStudios.Utilities.Memory`  
#### Inherits:
- `IList<T>`
## Summary
---
This provides a simple implementation of an array that scales in powers
of two. This is useful for quickly growing lists, such as those used in
the Compensation VR creation tools.

## Fields
---
### Arr
#### Type:
`public T[]`  
#### Summary
Primarily for internal use, this is the underlying array used for storage.  
Do not retain a reference to this - it could change when the array grows.

### Length
#### Type:
`public int`
#### Summary
The number of elements in the array.  
This is different from the **capacity**, which is the number of elements the
array can hold without expanding.

## Properties
---

### IsReadOnly
#### Type:
`public bool`  
#### Required by:
- `IList<T>.ICollection<T>`
#### Accessibility:
`get`  
#### Summary
Whether or not the array is read-only.  
This always returns `false` in this implementation.

### Count
#### Type:
`public int`  
#### Required by:  
- `IList<T>`
#### Accessibility:
`get`  
#### Summary
The number of elements in the array.  
Alias of `OverallocatingArray<T>.Length`.

### Capacity
#### Type:
`public int`  
#### Accessibility:
`get`
#### Summary
The number of elements the array can hold without expanding.  
Not to be confused with `Length`, which is the number of elements
actually present in the array.

## Indexers
---
### this\[\]
#### Type:
`public T`  
#### Required by:
`IList<T>`  
#### Arguments:  
- `int index`: The index of the element to get or set.
#### Exceptions:  
- `ArgumentOutOfRangeException`: Thrown when the index is
less than zero or greater than or equal to the length of the array.
#### Summary
Fetches an element from the array at the given index.

## Methods
---

### ToArray()
#### Type:
`public T[]`  
#### Returns:
`T[]`  
#### Type Arguments:  
- `T` (where `T: struct`): The type of structure.
#### Arguments:  
- `in T value`: The structure to convert to a byte array.
#### Notes:  
- This function uses the `MethodImpl` attribute for Aggressive Inlining.
#### Summary:
Copies the elements of the array to a new array. This is a shallow copy,
if `T` is a reference type then the references will remain the same.

---

### Add()
#### Type:
`public void`  
#### Required by:
`IList<T>.ICollection<T>`  
#### Arguments:  
- `T item`: The item to add to the array.
#### Notes:  
- This function uses the `MethodImpl` attribute for Aggressive Inlining.
#### Summary:  
Adds an item to the array.  

---

### EnsureCapacity()
#### Type:
`public void`  
#### Arguments:  
- `int capacity`: The minimum capacity to ensure.
#### Notes:  
- This function uses the `MethodImpl` attribute for Aggressive Inlining.
#### Summary:  
Ensures that the array has at least the given capacity. This capacity will be
rounded *up* to the closest power of two.

---

### Expand()
#### Type:
`public void`  
#### Notes:  
- This function uses the `MethodImpl` attribute for Aggressive Inlining.
#### Summary:  
Copies the backing array to a new one with with the capacity multiplied by
`GROWTH_FACTOR` (2).

---

### Clear()
#### Type:
- `public void`
#### Required by:
- `IList<T>.ICollection<T>`
#### Summary:  
Clears the array by setting its length to 0.  
This does not clear the backing array, nor does it change the capacity.

---

### Contains()  
#### Type:
`public bool`  
#### Required by:
`IList<T>.ICollection<T>`  
#### Arguments:  
- `T item`: The item to check for.
#### Summary:  
Checks if the array contains the given item.

---

### IndexOf()
#### Type:
`public int`  
#### Required by:
`IList<T>`  
#### Arguments:  
- `T item`: The item to check for.
#### Summary:  
Gets the first index of the given item in the array.  
Returns -1 if the item is not present.

---

### Insert()
#### Type:
`public void`  
#### Required by:
`IList<T>`  
#### Arguments:  
- `int index`: The index to insert at.
- `T item`: The item to insert.
#### Summary:  
Inserts an item into the array at the given index.

---

### Remove()
#### Type:
`public bool`  
#### Required by:
`IList<T>.ICollection<T>`  
#### Arguments:  
- `T item`: The item to remove.
#### Summary:
Removes the first instance of the given item from the array.

---

### RemoveAt()
#### Type:
`public void`  
#### Required by:
`IList<T>`  
#### Arguments:  
- `int index`: The index of the item to remove.
#### Summary:  
Removes the item at the given index from the array.

---

### CopyTo()
#### Type:
`public void`  
#### Required by:
`IList<T>.ICollection<T>`  
#### Arguments:  
- `T[] array`: The array to copy to.
- `int index`: The index to start copying at.
#### Summary:  
Copies the contents of the array to the given array, starting at the given
index.  
This is a shallow copy - any references will remain the same.

---

### IEnumerator.GetEnumerator()
#### Type:
`IEnumerator`  
#### Required by: 
- `IList<T>.ICollection<T>.IEnumerable<T>`
- `IList<T>.IEnumerable<T>`
#### Summary:  
Gets an enumerator for the array.

---

### IEnumerator\<T\>.GetEnumerator()
#### Type:
`IEnumerator<T>`  
#### Required by:
- `IList<T>.ICollection<T>.IEnumerable<T>`
- `IList<T>.IEnumerable<T>`
#### Summary:  
Gets an enumerator for the array.

## Static Methods
---

### WithCapacity()
#### Type:
`public static OverallocatingArray<T>`  
#### Type Arguments:  
- `T`: The type of item to be contained in the array.
#### Arguments:  
- `int capacity`: The initial capacity of the array.
#### Notes:  
- This function uses the `MethodImpl` attribute for Aggressive Inlining.
#### Summary:  
Creates a new array with the given capacity allocated immediately.
