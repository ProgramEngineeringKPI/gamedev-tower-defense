using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace strange.extensions.signal.impl
{
    /// Base concrete form for a Signal with three parameters
    public class Signal<T, U, V> : BaseSignal, ISignal
    {
        private readonly Dictionary<Action<T, U, V>, Action<T, U, V>> wrappersDictionary = new Dictionary<Action<T, U, V>, Action<T, U, V>>();

        public event Action<T, U, V> Listener = null;
        public event Action<T, U, V> OnceListener = null;

        public void AddListener(Action<T, U, V> callback)
        {
            Listener = this.AddUnique(Listener, callback);
        }

        public void AddOnce(Action<T, U, V> callback)
        {
            OnceListener = this.AddUnique(OnceListener, callback);
        }

        public void RemoveListener(Action<T, U, V> callback)
        {
            if (!this.wrappersDictionary.ContainsKey(callback))
            {
                Debug.LogWarning($"No such listener was added to signal: {callback.Method}");
                return;
            }

            if (Listener != null)
            {
                Listener -= this.wrappersDictionary[callback];
                this.wrappersDictionary.Remove(callback);
            }
        }
        public override List<Type> GetTypes()
        {
            List<Type> retv = new List<Type>();
            retv.Add(typeof(T));
            retv.Add(typeof(U));
            retv.Add(typeof(V));
            return retv;
        }
        public void Dispatch(T type1, U type2, V type3)
        {
            Listener?.Invoke(type1, type2, type3);
            OnceListener?.Invoke(type1, type2, type3);
            OnceListener = null;
            object[] outv = { type1, type2, type3 };
            base.Dispatch(outv);
        }
        private Action<T, U, V> AddUnique(Action<T, U, V> listeners, Action<T, U, V> callback, Action<T, U, V> originalAction = null)
        {
            if (listeners != null &&
                listeners.GetInvocationList().Contains(callback))
            {
                return listeners;
            }

            this.wrappersDictionary.Add(originalAction ?? callback, callback);

            listeners += callback;
            return listeners;
        }
        public override void RemoveAllListeners()
        {
            Listener = null;
            OnceListener = null;
            base.RemoveAllListeners();
        }
        public Delegate listener
        {
            get { return Listener ?? (Listener = delegate { }); }
            set { Listener = (Action<T,U,V>)value; }
        }

        public void AddListenerWhen(Func<T, U, V, bool> predicate, Action<T, U, V> callback)
        {
            Listener = this.AddUnique(Listener, (t, u, v) =>
            {
                if (predicate(t, u, v)) callback(t, u, v);
            }, callback);
        }

        public void AddOnceWhen(Func<T, U, V, bool> predicate, Action<T, U, V> callback)
        {
            throw new NotImplementedException();
//            Listener = this.AddUnique(Listener, (t, u, v) =>
//            {
//                if (predicate(t, u, v))
//                {
//                    callback(t, u, v);
//                    RemoveListener(callback);
//                }
//            });
        }

        public Task AwaitWhen(Func<T, U, V, bool> predicate)
        {
            throw new NotImplementedException();
//            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
//            this.AddOnceWhen(predicate, (t, u, v) => tcs.SetResult(0));
//            return tcs.Task;
        }
    }
}