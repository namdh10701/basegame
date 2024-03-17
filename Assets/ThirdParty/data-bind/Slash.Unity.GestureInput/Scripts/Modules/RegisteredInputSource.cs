// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisteredInputSource.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.GestureInput.Modules
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.EventSystems;

    public class RegisteredInputSource<TSource, TEventData> : RegisteredInputSource
        where TSource : class, IEventSystemHandler where TEventData : BaseEventData
    {
        /// <summary>
        ///   Shared list to avoid allocation.
        /// </summary>
        private static readonly List<TSource> Sources = new List<TSource>();

        private readonly Func<TSource, TEventData, bool> eventFunctor;

        public RegisteredInputSource(Func<TSource, TEventData, bool> eventFunctor)
        {
            this.eventFunctor = eventFunctor;
        }

        public override Type GetEventDataType()
        {
            return typeof(TEventData);
        }

        public override void Execute(BaseEventData eventData, IEnumerable<GameObject> currentPointedGameObjects)
        {
            foreach (var currentPointedGameObject in currentPointedGameObjects)
            {
                if (this.Execute((TEventData)eventData, currentPointedGameObject))
                {
                    return;
                }
            }
        }

        private bool Execute(TEventData eventData, GameObject root)
        {
            if (root == null)
            {
                return false;
            }

            var target = root.transform;
            var handled = false;
            while (target != null)
            {
                Sources.Clear();
                GetSources(target.gameObject, Sources);

                for (var i = 0; i < Sources.Count; i++)
                {
                    var source = Sources[i];
                    try
                    {
                        if (this.eventFunctor(source, eventData))
                        {
                            handled = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

                if (handled)
                {
                    break;
                }

                target = target.parent;
            }

            return handled;
        }

        private static void GetSources(GameObject go, ICollection<TSource> results)
        {
            if (results == null)
            {
                throw new ArgumentException("Results array is null", "results");
            }

            if (go == null || !go.activeInHierarchy)
            {
                return;
            }

            Components.Clear();
            go.GetComponents(Components);
            for (var i = 0; i < Components.Count; i++)
            {
                var component = Components[i];

                var source = component as TSource;
                if (source == null)
                {
                    continue;
                }

                var behaviour = component as Behaviour;
                if (behaviour != null && !behaviour.isActiveAndEnabled)
                {
                    continue;
                }

                results.Add(source);
            }
        }
    }

    public abstract class RegisteredInputSource
    {
        /// <summary>
        ///   Shared list to avoid allocation.
        /// </summary>
        protected static readonly List<Component> Components = new List<Component>();

        public abstract void Execute(BaseEventData eventData, IEnumerable<GameObject> currentPointedGameObjects);

        public abstract Type GetEventDataType();
    }
}