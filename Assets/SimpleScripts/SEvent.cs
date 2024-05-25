using System;
using System.Collections.Generic;

namespace App.SimplesScipts
{
    public sealed class SEvent : ISEvent
    {
        private readonly List<Action> _listeners = new();

        public void AddListener(Action action)
            => _listeners.Add(action);
        public void RemoveListener(Action action)
            => _listeners.Remove(action);
        public void ClearListeners()
            => _listeners.Clear();
        public void Invoke()
        {
            Action[] actions = _listeners.ToArray();
            foreach (Action action in actions)
                action?.Invoke();
        }
    }
    public interface ISEvent
    {
        public void AddListener(Action action);
        public void RemoveListener(Action action);
        public void ClearListeners();
    }
    public sealed class SEvent<T> : ISEvent<T>
    {
        private readonly List<Action<T>> _listeners = new();

        public void AddListener(Action<T> action)
            => _listeners.Add(action);
        public void RemoveListener(Action<T> action)
            => _listeners.Remove(action);
        public void ClearListeners()
            => _listeners.Clear();
        public void Invoke(T t)
        {
            Action<T>[] actions = _listeners.ToArray();
            foreach (Action<T> action in actions)
                action?.Invoke(t);
        }
    }

    public interface ISEvent<T>
    {
        public void AddListener(Action<T> action);
        public void RemoveListener(Action<T> action);
        public void ClearListeners();
    }
}