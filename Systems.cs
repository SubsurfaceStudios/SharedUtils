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

    public class YieldableTask<T> : CustomYieldInstruction
    {
        private readonly Task<T> await;
        public YieldableTask(Task<T> await) => this.await = await;

        public T result => await.Result;

        public override bool keepWaiting => !await.IsCompleted;
    public class YieldableTask : CustomYieldInstruction {
    {
        private readonly Task await;
        public YieldableTask(Task await) => this.await = await;
        public override bool keepWaiting => !await.IsCompleted;
    }
}
