using System;
using System.Collections;
using System.Collections.Generic;

namespace _Base.Scripts.EventSystem
{
    public delegate void GlobalCallback();
    public delegate void GlobalCallback<T>(T arg1);                          // 1 Argument
    public delegate void GlobalCallback<T, U>(T arg1, U arg2);               // 2 Arguments
    public delegate void GlobalCallback<T, U, V>(T arg1, U arg2, V arg3);    // 3 Arguments
    public delegate void GlobalCallback<T, U, V, X>(T arg1, U arg2, V arg3, X arg4);
    public delegate void GlobalCallback<T, U, V, X, R>(T arg1, U arg2, V arg3, X arg4, R arg5);
    public static class GlobalEvent
    {
        private static Hashtable m_Callbacks = new Hashtable();
        public static void Register(string name, GlobalCallback callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback> callbacks = (List<GlobalCallback>)m_Callbacks[name];
            if (callbacks == null)
            {
                callbacks = new List<GlobalCallback>();
                m_Callbacks.Add(name, callbacks);
            }
            callbacks.Add(callback);

        }


        /// <summary>
        /// Unregisters the event specified by name
        /// </summary>
        public static void Unregister(string name, GlobalCallback callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback> callbacks = (List<GlobalCallback>)m_Callbacks[name];
            if (callbacks != null)
                callbacks.Remove(callback);

        }

        public static void Send(string name)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            List<GlobalCallback> callbacks = (List<GlobalCallback>)m_Callbacks[name];
            if (callbacks != null)
                foreach (GlobalCallback c in callbacks)
                    c();
        }

    }

    public static class GlobalEvent<T>
    {

        private static Hashtable m_Callbacks = new Hashtable();

        public static void Register(string name, GlobalCallback<T> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T>> callbacks = (List<GlobalCallback<T>>)m_Callbacks[name];
            if (callbacks == null)
            {
                callbacks = new List<GlobalCallback<T>>();
                m_Callbacks.Add(name, callbacks);
            }
            callbacks.Add(callback);

        }

        public static void Unregister(string name, GlobalCallback<T> callback)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T>> callbacks = (List<GlobalCallback<T>>)m_Callbacks[name];
            if (callbacks != null)
                callbacks.Remove(callback);

        }

        public static void Send(string name, T arg1)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (arg1 == null)
                throw new ArgumentNullException("arg1");

            List<GlobalCallback<T>> callbacks = (List<GlobalCallback<T>>)m_Callbacks[name];
            if (callbacks != null)
                foreach (GlobalCallback<T> c in callbacks)
                    c(arg1);

        }

    }

    public static class GlobalEvent<T, U>
    {
        private static Hashtable m_Callbacks = new Hashtable();

        public static void Register(string name, GlobalCallback<T, U> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U>> callbacks = (List<GlobalCallback<T, U>>)m_Callbacks[name];
            if (callbacks == null)
            {
                callbacks = new List<GlobalCallback<T, U>>();
                m_Callbacks.Add(name, callbacks);
            }
            callbacks.Add(callback);

        }

        public static void Unregister(string name, GlobalCallback<T, U> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U>> callbacks = (List<GlobalCallback<T, U>>)m_Callbacks[name];
            if (callbacks != null)
                callbacks.Remove(callback);

        }

        public static void Send(string name, T arg1, U arg2)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (arg1 == null)
                throw new ArgumentNullException("arg1");

            if (arg2 == null)
                throw new ArgumentNullException("arg2");

            List<GlobalCallback<T, U>> callbacks = (List<GlobalCallback<T, U>>)m_Callbacks[name];
            if (callbacks != null)
                foreach (GlobalCallback<T, U> c in callbacks)
                    c(arg1, arg2);

        }

    }

    public static class GlobalEvent<T, U, V>
    {
        public static Hashtable m_Callbacks = new Hashtable();

        public static void Register(string name, GlobalCallback<T, U, V> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U, V>> callbacks = (List<GlobalCallback<T, U, V>>)m_Callbacks[name];
            if (callbacks == null)
            {
                callbacks = new List<GlobalCallback<T, U, V>>();
                m_Callbacks.Add(name, callbacks);
            }
            callbacks.Add(callback);

        }

        public static void Unregister(string name, GlobalCallback<T, U, V> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U, V>> callbacks = (List<GlobalCallback<T, U, V>>)m_Callbacks[name];
            if (callbacks != null)
                callbacks.Remove(callback);

        }

        public static void Send(string name, T arg1, U arg2, V arg3)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (arg1 == null)
                throw new ArgumentNullException("arg1");

            if (arg2 == null)
                throw new ArgumentNullException("arg2");

            if (arg3 == null)
                throw new ArgumentNullException("arg3");

            List<GlobalCallback<T, U, V>> callbacks = (List<GlobalCallback<T, U, V>>)m_Callbacks[name];
            if (callbacks != null)
                foreach (GlobalCallback<T, U, V> c in callbacks)
                    c(arg1, arg2, arg3);

        }

    }

    public static class GlobalEvent<T, U, V, X>
    {
        public static Hashtable m_Callbacks = new Hashtable();

        public static void Register(string name, GlobalCallback<T, U, V, X> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U, V, X>> callbacks = (List<GlobalCallback<T, U, V, X>>)m_Callbacks[name];
            if (callbacks == null)
            {
                callbacks = new List<GlobalCallback<T, U, V, X>>();
                m_Callbacks.Add(name, callbacks);
            }
            callbacks.Add(callback);

        }

        public static void Unregister(string name, GlobalCallback<T, U, V, X> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U, V, X>> callbacks = (List<GlobalCallback<T, U, V, X>>)m_Callbacks[name];
            if (callbacks != null)
                callbacks.Remove(callback);

        }

        public static void Send(string name, T arg1, U arg2, V arg3, X arg4)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");
            List<GlobalCallback<T, U, V, X>> callbacks = (List<GlobalCallback<T, U, V, X>>)m_Callbacks[name];
            if (callbacks != null)
                foreach (GlobalCallback<T, U, V, X> c in callbacks)
                    c(arg1, arg2, arg3, arg4);

        }

    }

    public static class GlobalEvent<T, U, V, X, R>
    {
        public static Hashtable m_Callbacks = new Hashtable();

        public static void Register(string name, GlobalCallback<T, U, V, X, R> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U, V, X, R>> callbacks = (List<GlobalCallback<T, U, V, X, R>>)m_Callbacks[name];
            if (callbacks == null)
            {
                callbacks = new List<GlobalCallback<T, U, V, X, R>>();
                m_Callbacks.Add(name, callbacks);
            }
            callbacks.Add(callback);

        }

        public static void Unregister(string name, GlobalCallback<T, U, V, X, R> callback)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");

            if (callback == null)
                throw new ArgumentNullException("callback");

            List<GlobalCallback<T, U, V, X, R>> callbacks = (List<GlobalCallback<T, U, V, X, R>>)m_Callbacks[name];
            if (callbacks != null)
                callbacks.Remove(callback);

        }

        public static void Send(string name, T arg1, U arg2, V arg3, X arg4, R arg5)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(@"name");
            List<GlobalCallback<T, U, V, X, R>> callbacks = (List<GlobalCallback<T, U, V, X, R>>)m_Callbacks[name];
            if (callbacks != null)
                foreach (GlobalCallback<T, U, V, X, R> c in callbacks)
                    c(arg1, arg2, arg3, arg4, arg5);

        }

    }
}