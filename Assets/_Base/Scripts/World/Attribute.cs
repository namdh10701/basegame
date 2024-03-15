namespace _Base.Scripts.World
{
    public interface IAttribute
    {
        
    }
    
    public abstract class Attribute<T>: IAttribute
    {
        public T Value { get; set; }
    }
}