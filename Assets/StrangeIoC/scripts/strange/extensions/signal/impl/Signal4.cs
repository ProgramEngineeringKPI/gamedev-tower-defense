using System;
using System.Collections.Generic;
using System.Linq;

namespace strange.extensions.signal.impl
{
    /// Base concrete form for a Signal with four parameters
    public class Signal<T, U, V, W> : BaseSignal, ISignal
    {
        public event Action<T, U, V, W> Listener = null;
        public event Action<T, U, V, W> OnceListener = null;

        public void AddListener(Action<T, U, V, W> callback)
        {
            Listener = this.AddUnique(Listener, callback);
        }

        public void AddOnce(Action<T, U, V, W> callback)
        {
            OnceListener = this.AddUnique(OnceListener, callback);
        }

        public void RemoveListener(Action<T, U, V, W> callback)
        {
            if (Listener != null)
                Listener -= callback;
        }
        public override List<Type> GetTypes()
        {
            List<Type> retv = new List<Type>();
            retv.Add(typeof(T));
            retv.Add(typeof(U));
            retv.Add(typeof(V));
            retv.Add(typeof(W));
            return retv;
        }
        public void Dispatch(T type1, U type2, V type3, W type4)
        {
            if (Listener != null)
                Listener(type1, type2, type3, type4);
            if (OnceListener != null)
                OnceListener(type1, type2, type3, type4);
            OnceListener = null;
            object[] outv = { type1, type2, type3, type4 };
            base.Dispatch(outv);
        }

        private Action<T, U, V, W> AddUnique(Action<T, U, V, W> listeners, Action<T, U, V, W> callback)
        {
            if (listeners == null || !listeners.GetInvocationList().Contains(callback))
            {
                listeners += callback;
            }
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
            set { Listener = (Action<T,U,V,W>)value; }
        }
    }
}