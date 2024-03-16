using System;

namespace _Base.Scripts.RPG
{
    public interface IEntity
    {
        TAttribute GetAttribute<TAttribute>() where TAttribute: IAttribute;
    }
}