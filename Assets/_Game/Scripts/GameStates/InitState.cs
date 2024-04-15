﻿using System.Collections;
using _Base.Scripts.StateMachine;
using _Game.Scripts.Bootstrap;
using UnityEngine;

namespace _Game.Scripts.GameStates
{
    /// <summary>
    /// Delays the state-machine for the set amount
    /// </summary>
    public class InitState : AbstractState
    {
        public override string Name => nameof(InitState);

        public override IEnumerator Execute()
        {
            Game.Instance.AssetLoader.Load();
            Game.Instance.GameManager.LoadDatabase();
            Game.Instance.GameManager.LoadSave();
            yield return null;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
