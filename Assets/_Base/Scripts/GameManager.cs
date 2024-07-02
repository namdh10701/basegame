using _Base.Scripts.Utils;
using Map;
namespace _Base.Scripts
{
    public abstract class BaseGameManager : SingletonMonoBehaviour<BaseGameManager>
    {
        public abstract void LoadDatabase();
    }
}
