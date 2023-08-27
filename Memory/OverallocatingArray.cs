using SubsurfaceStudios.Utilities.Math;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;

namespace SubsurfaceStudios.Utilities.Memory {
    using System;

    public class OverallocatingArray<T> : IList<T> {
        public T[] Arr;
        public int Length;

        const float GROWTH_THRESHOLD = 0.8f;
        const float GROWTH_FACTOR = 2;

        public bool IsReadOnly => false;

        public int Count => Length;

        public int Capacity => Arr.Length;

        public T this[int index] { 
            get {
                if(index >= Length || index < 0) throw new IndexOutOfRangeException($"Attempted to get an index into OverallocatingArray<{typeof(T).Name}> at index {index}, which is outside the available range of 0..={Length - 1}");
                return Arr[index];
            }
            set {
                if(index >= Length || index < 0) throw new IndexOutOfRangeException($"Attempted to set an index into OverallocatingArray<{typeof(T).Name}> at index {index}, which is outside the available range of 0..={Length - 1}");
                Arr[index] = value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray() => Arr[0..Length];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item) {
            if(Length >= Arr.Length * GROWTH_THRESHOLD) Expand();

            Arr[Length++] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int capacity) {
            if(Arr.Length >= capacity) return;

            uint cap = MathUtils.NextPowerOfTwo((uint)capacity);
            T[] n = new T[cap];
            Array.Copy(Arr, n, Arr.Length);
            Arr = n;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Expand() {
            int cap = Convert.ToInt32(Math.Ceiling(Arr.Length * GROWTH_FACTOR));
            T[] n = new T[cap];
            Array.Copy(Arr, n, Arr.Length);
            Arr = n;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OverallocatingArray<T> WithCapacity(int capacity) {
            return new OverallocatingArray<T>() {
                Arr = new T[capacity],
                Length = 0
            };
        }

        public void Clear() => Length = 0;

        public bool Contains(T value)
        {
            for (int i = 0; i < Length; ++i)
                if(Arr[i].Equals(value)) return true;

            return false;
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < Length; ++i)
                if(Arr[i].Equals(value)) return i;

            return -1;
        }

        public void Insert(int index, T value)
        {
            EnsureCapacity(Length);

            for (int i = Length - 2; i >= index ; --i)
                Arr[i + 1] = Arr[i];

            Arr[index] = value;
        }

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if(index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            Length -= 1;
            for (int i = index; i <= Length; i++)
                Arr[i] = Arr[i + 1];
        }

        public void CopyTo(T[] array, int index)
        {
            Array.Copy(Arr, index, array, index, Length);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            for (int i = 0; i < Length; i++)
                yield return Arr[i];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < Length; ++i)
                yield return Arr[i];
        }
    }
}
