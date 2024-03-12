using _Base.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.Bootstrap
{
    [DisallowMultipleComponent]
    public abstract class BaseGame : SingletonMonoBehaviour<BaseGame>, IBootLoader
    {
        [field:SerializeReference]
        public SequenceManager SequenceManager { get; set; }
        
        [field:SerializeReference]
        public SceneController SceneController { get; set; }
        
        [field:SerializeReference]
        public AssetLoader AssetLoader { get; set; }
        
        [field:SerializeReference]
        public BaseGameManager GameManager { get; set; }

        public void Start()
        {
            Assert.IsNotNull(SequenceManager);
            SequenceManager.Initialize();
        }
    }
}
