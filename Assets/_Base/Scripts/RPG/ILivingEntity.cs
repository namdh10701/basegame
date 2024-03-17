using System;
using _Game.Scripts.Attributes;

namespace _Base.Scripts.RPG
{
    public interface ILivingEntity
    {
        HealthPoint HealthPoint { get; set; }
    }
}