using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace strange.extensions.signal.impl
{
    /// Base concrete form for a Signal with one parameter
    public class Signal<T> : BaseSignal, ISignal
    {
        private readonly Dictionary<Action<T>, Action<T>> wrappersDictionary = new Dictionary<Action<T>, Action<T>>();

        public event Action<T> Listener = null;
        public event Action<T> OnceListener = null;

        public void TryAddListener(Action<T> callback)
        {
            if (this.wrappersDictionary.ContainsKey(callback))
            {
                return;
            }
            Listener = this.AddUnique(Listener, callback);
        }

        public void AddListener(Action<T> callback)
        {
            Listener = this.AddUnique(Listener, callback);
        }
        
        public void AddOnce(Action<T> callback)
        {
            OnceListener = this.AddUnique(OnceListener, callback);
        }

        public void RemoveListener(Action<T> callback)
        {
            if (!this.wrappersDictionary.ContainsKey(callback))
            {
#if DEBUG
                //this is not error, only warning about unsubscribe without actual subscribe
                Debug.LogWarning($"No such listener was added to signal: {callback.Method}");
#endif
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
            return retv;
        }

        public void Dispatch(T type1)
        {
            Listener?.Invoke(type1);

            OnceListener?.Invoke(type1);
            OnceListener = null;

            object[] outv = {type1};
            base.Dispatch(outv);
        }

        private Action<T> AddUnique(Action<T> listeners, Action<T> callback, Action<T> originalAction = null)
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
            set { Listener = (Action<T>) value; }
        }

//        public void AddListenerWhen(Func<T, bool> predicate, Action<T> callback)
//        {
//            Listener = this.AddUnique(Listener, (t) =>
//            {
//                if (predicate(t)) callback(t);
//            }, callback);
//        }
//
//        public void AddOnceWhen(Func<T, bool> predicate, Action<T> callback)
//        {
//            Listener = this.AddUnique(Listener, predicatedCallBack, callback);
//            
//            void predicatedCallBack(T t)
//            {
//                if (predicate(t))
//                {
//                    callback(t);
//                    RemoveListener(callback);
//                }
//            }
//        }
//
//        public Task AwaitWhen(Func<T, bool> predicate)
//        {
//            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
//            this.AddOnceWhen(predicate, (t) => tcs.SetResult(t));
//            return tcs.Task;
//        }
    }
}