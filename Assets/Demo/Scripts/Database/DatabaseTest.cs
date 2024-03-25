using _Game.Scripts.Database;
using UnityEngine;
namespace Demo.Scripts
{
    public class DatabaseTest : MonoBehaviour
    {
        [SerializeField] Database Database;
        private void Awake()
        {
            Database.Load();
            Debug.Log("Monster Name: " + Database.Monster.GetById(1).Name);
            Debug.Log("Hero Atk Type: " + Database.Hero.GetById(1).AtkDef.Type);
        }
    }
}