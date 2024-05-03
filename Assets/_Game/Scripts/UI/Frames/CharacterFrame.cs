using _Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFrame : MonoBehaviour
{
    [SerializeField] private Image characterImg;
    public void SetData(CharacterDefinition characterDefinition)
    {
        Sprite image = ResourceLoader.LoadCharacterImage(characterDefinition.Id);
    }
}
