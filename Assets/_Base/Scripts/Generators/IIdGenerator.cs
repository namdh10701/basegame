namespace _Base.Scripts.Generators
{
    /// <summary>
    /// ID generator
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Get next ID
        /// </summary>
        /// <returns></returns>
        public string Next();
    }
}