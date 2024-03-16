using System;
using Slash.Unity.DataBind.Core.Presentation;
using Slash.Unity.DataBind.Foundation.Providers.Formatters;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public class AttributeFormatter: Formatter<string, object>
    {
        #region Fields

        /// <summary>
        ///   Data value to convert to upper-case.
        /// </summary>
        public DataBinding Argument;

        #endregion

        #region Public Properties

        public override object Value
        {
            get
            {
                // return "xxx";
                // Get data to convert.
                var argument = this.Argument.GetValue<Attribute>();

                // Convert to upper-case.                               
                return argument?.GetValue()?.ToString();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Awake()
        {
            // Add bindings.
            this.AddBinding(this.Argument);
        }

        protected override bool UpdateTarget(string target, object value)
        {
            target = value?.ToString();
            return true;
        }

        protected override void UpdateValue()
        {
            // Convert value and notify interested listeners.
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}