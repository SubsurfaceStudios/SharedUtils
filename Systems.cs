using UnityEngine;

namespace SubsurfaceStudios.UI.Systems {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UnityEngine;

    [Serializable]
    /// <summary>
    /// A system for iterating over an array in "pages", fixed-size slices of the backing array.
    /// </summary>
    /// <typeparam name="T">The type to iterate over. Must be nullable. (no structs)</typeparam>
    public sealed class PagedList<T> where T : class {
        public ReadOnlyCollection<T> Items {
            get {
                if(items_cache == null) items_cache = new ReadOnlyCollection<T>(items);
                return items_cache;
            }
        }
        private ReadOnlyCollection<T> items_cache;
        private IList<T> items = new List<T>();
        private int itemsPerPage = 6;
        private int pageIndex = 0;

        public int PageIndex {
            get => pageIndex == 0 ? 1 : (pageIndex / itemsPerPage);
            set => pageIndex = value * itemsPerPage;
        }

        public int PageCount {
            get => Mathf.CeilToInt(items.Count / itemsPerPage)+1;
        }

        /// <summary>
        /// Create a new PagedList from a list and a number of items per page.
        /// </summary>
        /// <param name="items">The IList to iterate over.</param>
        /// <param name="itemsPerPage">The number of items per page.</param>
        public PagedList(IList<T> items, int itemsPerPage) {
            this.items = items;
            this.itemsPerPage = itemsPerPage;
            this.pageIndex = 0;
        }

        private PagedList() {} // Prevent instantiation without arguments.

        /// <summary>
        /// Updates the items in this PagedList.
        /// </summary>
        /// <param name="items">The new item array.</param>
        /// <param name="resetIndex">Whether or not to reset the page index. Defaults to true.</param>
        public void RefreshItems(IList<T> items, bool resetIndex = true) {
            this.items = items;
            if(resetIndex) pageIndex = 0;
        }

        /// <summary>
        /// Increment the current page index.
        /// </summary>
        /// <returns>Whether or not the page index can be/was incremented.</returns>
        public bool IncrementPage() {
            if(!CanIncrementPage) return false;

            pageIndex += itemsPerPage;

            return true;
        }

        /// <summary>
        /// Decrement the current page index.
        /// </summary>
        /// <returns>Whether or not the page index can be/was decremented.</returns>
        public bool DecrementPage() {
            if(!CanDecrementPage) return false;

            pageIndex -= itemsPerPage;

            return true;
        }

        /// <summary>
        /// Resets the current page index to 0.
        /// </summary>
        public void ResetPage() {
            pageIndex = 0;
        }

        /// <summary>
        /// Adds an item to this PagedArray.
        /// </summary>
        /// <param name="item">The item to add. No duplicate protection!</param>
        public void AddItem(T item) {
            items.Add(item);
        }

        /// <summary>
        /// Removes an item from this PagedArray.
        /// </summary>
        /// <param name="item">The item to remove. Will not cause errors if the item is not in this PagedArray.</param>
        public void RemoveItem(T item) {
            items.Remove(item);
        }

        public void AddRange(IList<T> item) {
            for (int i = 0; i < item.Count; i++)
                items.Add(item[i]);
        }

        /// <summary>
        /// Whether or not you can run the IncrementPage method successfully. Useful for UI checks.
        /// </summary>
        public bool CanIncrementPage { get => pageIndex + itemsPerPage < items.Count; }
        /// <summary>
        /// Whether or not you can run the DecrementPage method successfully. Useful for UI checks.
        /// </summary>
        public bool CanDecrementPage { get => pageIndex - itemsPerPage >= 0; }

        /// <summary>
        /// Reads a page of items from the array.
        /// </summary>
        /// <remarks>
        /// You must handle null objects when reading from this array, as it is not done internally.
        /// </remarks>
        /// <returns>A read-only collection of the items in this page.</returns>
        public ReadOnlyCollection<T> Page() {
            List<T> x = new List<T>(itemsPerPage);

            for(
                int i = pageIndex; 
                i < pageIndex + itemsPerPage; 
                i++
            ) {
                if(items.Count <= i) x.Add(null);
                else x.Add(items[i] ?? null);
            }

            return new ReadOnlyCollection<T>(x);
        }

        public int[] PageIndices() {
            int[] x = new int[itemsPerPage];

            int a = 0;
            for(
                int i = pageIndex * itemsPerPage; 
                i < pageIndex * itemsPerPage + itemsPerPage; 
                i++
            ) {
                x[a] = i;
                a++;
            }

            return x;
        }
    
        public void ForceUpdateInternalList(IList<T> items) {
            this.items = items;
            this.items_cache = null;
        }
    }    
}

namespace SubsurfaceStudios.Utilities.Async {
    using System.Threading.Tasks;
    using UnityEngine;
    using System.Collections;

    public class YieldableTask<T> : CustomYieldInstruction
    {
        private Task<T> await;
        public YieldableTask(Task<T> await) => this.await = await;

        public T result => await.Result;

        public override bool keepWaiting => !await.IsCompleted;
    }
    public class YieldableTask : CustomYieldInstruction
    {
        private Task await;
        public YieldableTask(Task await) => this.await = await;
        public override bool keepWaiting => !await.IsCompleted;
    }
}

namespace SubsurfaceStudios.Utilities.Memory {
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using UnityEngine;

    public class OverallocatingArray<T> : IList<T> {
        public T[] Arr;
        public int Length = 1;

        const float GROWTH_THRESHOLD = 0.8f;
        const float GROWTH_FACTOR = 2;

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Count => Length;

        public bool IsSynchronized => false;

        public int Capacity => Arr.Length;

        public object SyncRoot { get; } = new object();

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
        public void Fill(int start, int end, T value) {
            EnsureCapacity(end);

            for (int i = start; i < end; i++)
                Arr[i] = value;

            Length = end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int capacity) {
            if(Arr.Length >= capacity) return;

            int cap = Mathf.NextPowerOfTwo(capacity);
            T[] n = new T[cap];
            Array.Copy(Arr, n, Arr.Length);
            Arr = n;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Expand() {
            int cap = Mathf.CeilToInt(Arr.Length * GROWTH_FACTOR);
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
