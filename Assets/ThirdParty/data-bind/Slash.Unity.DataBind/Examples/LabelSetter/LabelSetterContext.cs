// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelSetterContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.LabelSetter
{
    using Slash.Unity.DataBind.Core.Data;

    /// <summary>
    ///   Data context for LabelSetter example.
    /// </summary>
    public class LabelSetterContext : Context
    {
        #region Fields

        /// <summary>
        ///   Data binding property, used to get informed when a data change happened.
        /// </summary>
        private readonly Property<string> textProperty = new Property<string>();

        #endregion


        #region Properties

        /// <summary>
        ///   Text value to visualize.
        /// </summary>
        public string Text
        {
            get
            {
                return this.textProperty.Value;
            }
            set
            {
                this.textProperty.Value = value;
            }
        }

        #endregion
    }
}