using _Game.Scripts.CrewSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class CrewFrame : MonoBehaviour
    {
        [SerializeField] private Image characterImg;
        public void SetData(CrewData characterDefinition)
        {
           Sprite image = ResourceLoader.LoadCharacterImage(characterDefinition.Id);
        }
    }
}
