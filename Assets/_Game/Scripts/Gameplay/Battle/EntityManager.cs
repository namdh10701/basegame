using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Gameplay
{
    public class EntityManager : MonoBehaviour
    {
        public List<Entity> aliveEntities = new List<Entity>();
        public Entity SpawnEntity(Entity entity, Vector3 position, Quaternion rotation, Transform parent)
        {
            Entity spawnedEntity = Instantiate(entity, position, rotation, parent);
            if (spawnedEntity.Stats is IAliveStats alive)
            {
                aliveEntities.Add(spawnedEntity);

                alive.MaxHealthPoint.OnValueChanged += (newStat) => MaxHealthPoint_OnValueChanged(newStat, spawnedEntity);
            }
            return spawnedEntity;

        }

        private void MaxHealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.Stat newStat, Entity alive)
        {
            if (newStat.Value <= newStat.MinValue)
            {
                aliveEntities.Remove((Entity)alive);
                GlobalEvent<Entity>.Send("EntityDied", alive);
            }
        }

        public void OnEntityHpReach0()
        {

        }
    }
}