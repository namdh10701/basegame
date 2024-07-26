using System.Threading;

namespace _Base.Scripts.Generators
{
    /// <summary>
    /// 
    /// </summary>
    public class SequenceGenerator: IIdGenerator
    {
        private static int _counter = 0;
        private readonly string m_prefix;

        public SequenceGenerator(string prefix = "")
        {
            m_prefix = prefix;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Next()
        {
            Interlocked.Increment(ref _counter);
            return m_prefix + _counter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed"></param>
        public void SetSeed(int seed)
        {
            _counter = seed;
        }
    }
}