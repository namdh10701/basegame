// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerExampleInputModule.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.GestureInput.Examples.TriggerExample
{
    using Slash.Unity.GestureInput.Gestures.Implementations;
    using Slash.Unity.GestureInput.Modules;

    public class TriggerExampleInputModule : GestureInputModule
    {
        public MousePointerDevice MousePointer;

        public TriggerGestureRecognizer TriggerGestureRecognizer;

        public override void ActivateModule()
        {
            base.ActivateModule();

            this.MousePointer = new MousePointerDevice();
            this.WorldPointer = this.MousePointer;

            this.TriggerGestureRecognizer.Init(this.MousePointer, this.PointerTouchDetection);
            this.AddRecognizer(this.TriggerGestureRecognizer);
        }
    }
}