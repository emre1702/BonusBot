using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace BonusBot.AudioModule.LavaLink
{
    public delegate void QueueChangedDelegate();

    internal class LavaQueue<T>
    {
        public bool IsShuffle
        {
            get => _isShuffle;
            set
            {
                _isShuffle = value;
                if (_isShuffle)
                    Shuffle();
            }
        }

        public int Count
        {
            get
            {
                lock (_list) return _list.Count;
            }
        }

        public List<T> Items
        {
            get
            {
                lock (_list)
                {
                    return _list.ToList();
                }
            }
        }

        public event QueueChangedDelegate? QueueChanged;

        private bool _isShuffle;
        private List<T> _list = new();
        private readonly Random _random = new();
        private readonly StringBuilder builder = new();

        public void Enqueue(T item)
        {
            if (IsShuffle)
                EnqueueRandom(item);
            else
            {
                lock (_list) _list.Add(item);
                QueueChanged?.Invoke();
            }
        }

        public void EnqueueRandom(T item)
        {
            lock (_list)
            {
                var index = _random.Next(_list.Count);
                _list.Insert(index, item);
                QueueChanged?.Invoke();
            }
        }

        public T? Dequeue()
        {
            lock (_list)
            {
                var firstValue = _list.FirstOrDefault();
                if (firstValue is null)
                    return default;
                _list.RemoveAt(0);
                QueueChanged?.Invoke();
                return firstValue;
            }
        }

        public bool TryDequeue([NotNullWhen(true)] out T? item)
        {
            item = Dequeue();
            return item is { };
        }

        public T Peek()
        {
            lock (_list) return _list[0];
        }

        public bool Remove(T item)
        {
            var removed = false;
            lock (_list)
            {
                removed = _list.Remove(item);
            }

            if (removed)
                QueueChanged?.Invoke();
            return removed;
        }

        public void Clear()
        {
            lock (_list)
            {
                if (_list.Count > 0)
                {
                    _list.Clear();
                    QueueChanged?.Invoke();
                }
            }
        }

        public T? RemoveAt(int index)
        {
            lock (_list)
            {
                if (_list.Count <= index)
                    return default;
                var item = _list[index];
                _list.RemoveAt(index);
                QueueChanged?.Invoke();
                return item;
            }
        }

        private void Shuffle()
        {
            lock (_list)
            {
                _list = _list.Shuffle().ToList();
                QueueChanged?.Invoke();
            }
        }

        public override string ToString()
        {
            lock (builder)
                lock (Items)
                {
                    for (var i = 0; i < Items.Count; ++i)
                        builder.AppendLine($"{i + 1}. {Items[i]}");
                    var str = builder.ToString();
                    builder.Clear();
                    return str;
                }
        }
    }
}