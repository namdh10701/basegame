using _Base.Scripts.RPG.Attributes;

namespace _Base.Scripts.RPG.Entities
{
    public interface IEntity
    {
        TAttribute GetAttribute<TAttribute>() where TAttribute: IAttribute;
    }
}