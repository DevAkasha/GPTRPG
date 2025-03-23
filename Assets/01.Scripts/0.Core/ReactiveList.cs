using System;
using System.Collections;
using System.Collections.Generic;

namespace Akasha
{
    public record ReactiveListChange<T>(ListChangeType ChangeType, int Index, T? Item);

    [Serializable]
    public class ReactiveList<T> : IList<T>
    {
        public event Action<ReactiveListChange<T>>? OnChangedDetailed;
        private readonly List<T> _list = new();
        public event Action OnChanged;

        public T this[int index]
        {
            get => _list[index];
            set
            {
                _list[index] = value;
                OnChanged?.Invoke();
            }
        }

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _list.Add(item);
            OnChangedDetailed?.Invoke(new ReactiveListChange<T>(ListChangeType.Add, _list.Count - 1, item));
            OnChanged?.Invoke();
        }

        public bool Remove(T item)
        {
            int index = _list.IndexOf(item);
            bool removed = _list.Remove(item);
            if (removed)
            {
                OnChanged?.Invoke();
                OnChangedDetailed?.Invoke(new ReactiveListChange<T>(ListChangeType.Remove, index, item));
            }
            return removed;
        }

        public void Clear()
        {
            _list.Clear();
            OnChangedDetailed?.Invoke(new ReactiveListChange<T>(ListChangeType.Clear, -1, default));
            OnChanged?.Invoke();
        }

        public bool Contains(T item) => _list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int IndexOf(T item) => _list.IndexOf(item);

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            OnChangedDetailed?.Invoke(new ReactiveListChange<T>(ListChangeType.Insert, index, item));
            OnChanged?.Invoke();
        }

        public void RemoveAt(int index)
        {
            var removedItem = _list[index];
            _list.RemoveAt(index);
            OnChangedDetailed?.Invoke(new ReactiveListChange<T>(ListChangeType.Remove, index, removedItem));
            OnChanged?.Invoke();
        }

        public void ReactiveForeach(Func<T, T> action)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i] = action(_list[i]);
            }
            OnChangedDetailed?.Invoke(new ReactiveListChange<T>(ListChangeType.Replace, -1, default));
            OnChanged?.Invoke();
        }

    }

}