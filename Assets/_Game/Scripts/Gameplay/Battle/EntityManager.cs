using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections.Generic;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

namespace _Game.Scripts.Battle
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

                alive.HealthPoint.OnValueChanged += (newStat) => MaxHealthPoint_OnValueChanged(newStat, spawnedEntity);
            }
            return spawnedEntity;

        }

        private void MaxHealthPoint_OnValueChanged(RangedStat newStat, Entity alive)
        {
            if (newStat.Value <= 0)
            {
                aliveEntities.Remove(alive);
                GlobalEvent<Entity>.Send("EntityDied", alive);
            }
        }

        public void OnEntityHpReach0()
        {

        }
    }
}