// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureInputModule.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.GestureInput.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Slash.Unity.GestureInput.Devices;
    using Slash.Unity.GestureInput.Gestures;
    using Slash.Unity.GestureInput.Raycasts;

    using UnityEngine;
    using UnityEngine.EventSystems;

    public class GestureInputModule : PointerInputModule
    {
        private readonly Dictionary<Type, RegisteredInputSource> registeredInputSources =
            new Dictionary<Type, RegisteredInputSource>();

        private List<GestureRecognizer> gestureRecognizers;

        private PointerEventData pointerEventData;

        private PointerTouchDetection pointerTouchDetection;

        public IPointerDevice WorldPointer { get; set; }

        protected IPointerTouchDetection PointerTouchDetection
        {
            get
            {
                return this.pointerTouchDetection;
            }
        }

        public override void ActivateModule()
        {
            base.ActivateModule();

            this.pointerTouchDetection = new PointerTouchDetection(this.eventSystem);
        }

        public override void Process()
        {
            this.UpdatePointerEventData();

            this.ProcessMove(this.pointerEventData);
            this.ProcessDrag(this.pointerEventData);

            foreach (var gestureRecognizer in this.gestureRecognizers)
            {
                gestureRecognizer.Process();
            }
        }

        private void AddInputSource(RegisteredInputSource inputSource)
        {
            this.registeredInputSources[inputSource.GetEventDataType()] = inputSource;
        }

        protected void AddRecognizer(GestureRecognizer gestureRecognizer)
        {
            gestureRecognizer.GestureDetected += this.OnGestureDetected;
            this.gestureRecognizers.Add(gestureRecognizer);

            foreach (var inputSource in gestureRecognizer.GetInputSources())
            {
                this.AddInputSource(inputSource);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            this.gestureRecognizers = new List<GestureRecognizer>();
        }

        private void OnGestureDetected(BaseEventData eventData)
        {
            if (this.WorldPointer == null)
            {
                return;
            }

            var pointedGameObjects =
                this.pointerTouchDetection.GetTouchedGameObjects(this.WorldPointer.GetPosition()).ToList();
            if (!pointedGameObjects.Any())
            {
                return;
            }

            var eventType = eventData.GetType();
            RegisteredInputSource registeredInputSource;
            if (!this.registeredInputSources.TryGetValue(eventType, out registeredInputSource))
            {
                Debug.LogFormat("No registered input source found for input event '{0}'", eventType);
                return;
            }

            // Handle event.
            registeredInputSource.Execute(eventData, pointedGameObjects);
        }

        private void UpdatePointerEventData()
        {
            if (this.pointerEventData == null)
            {
                this.pointerEventData = new PointerEventData(this.eventSystem);
            }

            if (this.WorldPointer != null)
            {
                this.pointerEventData.position = this.WorldPointer.GetPosition();
                var raycastResults = new List<RaycastResult>();
                this.eventSystem.RaycastAll(this.pointerEventData, raycastResults);
                this.pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
            }
        }
    }
}