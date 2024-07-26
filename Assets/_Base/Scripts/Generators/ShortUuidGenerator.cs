using shortid;
using shortid.Configuration;

namespace _Base.Scripts.Generators
{
    /// <summary>
    /// 
    /// </summary>
    public class ShortUuidGenerator: IIdGenerator
    {
        private static readonly GenerationOptions Options = new(length: 8, useSpecialCharacters: false, useNumbers: true);
        private readonly string _prefix;

        public ShortUuidGenerator(string prefix = "")
        {
            _prefix = prefix;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Next()
        {
            return _prefix + ShortId.Generate(Options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed"></param>
        public void SetSeed(int seed)
        {
            ShortId.SetSeed(seed);
        }
    }
}