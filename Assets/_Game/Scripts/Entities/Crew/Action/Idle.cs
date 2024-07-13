using _Game.Scripts;
using System.Collections;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class Idle : CrewActionBase
    {
        Crew crew;
        public Idle(Crew crew)
        {
            Name = "IDLE";
            this.crew = crew;
            this.Execute = DoExecute();
        }
        public IEnumerator DoExecute()
        {
            crew.Animation.AddIdle();
            yield return new WaitForSeconds(2);
        }
        public override void Interupt()
        {

        }

        public override void ReBuild(Crew crew)
        {
            this.Execute = DoExecute();
        }
    }
}
