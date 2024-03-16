// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoWayBindingExampleCharacterContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.TwoWayBinding
{
    using Slash.Unity.DataBind.Core.Data;

    public class CharacterContext : Context
    {
        private readonly Property<int> ageProperty =
            new();

        private readonly Property<float> charismaProperty =
            new();

        private readonly Property<string> nameProperty =
            new();

        private readonly Property<double> strengthProperty =
            new();
 
        public int Age
        {
            get
            {
                return ageProperty.Value;
            }
            set
            {
                ageProperty.Value = value;
            }
        }

        public float Charisma
        {
            get
            {
                return charismaProperty.Value;
            }
            set
            {
                charismaProperty.Value = value;
            }
        }

        public string Name
        {
            get
            {
                return nameProperty.Value;
            }
            set
            {
                nameProperty.Value = value;
            }
        }

        public double Strength
        {
            get
            {
                return strengthProperty.Value;
            }
            set
            {
                strengthProperty.Value = value;
            }
        }
    }
}