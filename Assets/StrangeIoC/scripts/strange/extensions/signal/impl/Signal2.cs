using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace strange.extensions.signal.impl
{
    /// Base concrete form for a Signal with two parameters
    public class Signal<T, U> : BaseSignal, ISignal
    {
        private readonly Dictionary<Action<T, U>, Action<T, U>> wrappersDictionary = new Dictionary<Action<T, U>, Action<T, U>>();

        public event Action<T, U> Listener = null;
        public event Action<T, U> OnceListener = null;

        public void AddListener(Action<T, U> callback)
        {
            Listener = this.AddUnique(Listener, callback);
        }

        public void AddOnce(Action<T, U> callback)
        {
            OnceListener = this.AddUnique(OnceListener, callback);
        }

        public void RemoveListener(Action<T, U> callback)
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
            return retv;
        }
        public void Dispatch(T type1, U type2)
        {
            Listener?.Invoke(type1, type2);
            OnceListener?.Invoke(type1, type2);
            OnceListener = null;
            object[] outv = { type1, type2 };
            base.Dispatch(outv);
        }

        private Action<T, U> AddUnique(Action<T, U> listeners, Action<T, U> callback, Action<T, U> originalAction = null)
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
            set { Listener = (Action<T,U>)value; }
        }
//        
//        public void AddListenerWhen(Func<T, U, bool> predicate, Action<T, U> callback)
//        {
//            Listener = this.AddUnique(Listener, (t, u) =>
//            {
//                if (predicate(t, u)) callback(t, u);
//            });
//        }
//
//        public void AddOnceWhen(Func<T, U, bool> predicate, Action<T, U> callback)
//        {
//            Listener = this.AddUnique(Listener, predicatedCallBack);
//
//            void predicatedCallBack(T t, U u)
//            {
//                if (predicate(t, u))
//                {
//                    callback(t, u);
//                    RemoveListener(callback);
//                }
//            }
//        }
//
//        public Task AwaitWhen(Func<T, U, bool> predicate)
//        {
//            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
//            this.AddOnceWhen(predicate, (t, u) => tcs.SetResult(0));
//            return tcs.Task;
//        }
    }
}