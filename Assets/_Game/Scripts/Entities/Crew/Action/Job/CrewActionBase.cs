using _Game.Scripts;
using System;
using System.Collections;
namespace _Game.Features.Gameplay
{
    [Serializable]
    public abstract class CrewActionBase
    {
        public string Name;
        public IEnumerator Execute;
        public abstract void Interupt();
        public abstract void ReBuild(Crew crew);
    }
}