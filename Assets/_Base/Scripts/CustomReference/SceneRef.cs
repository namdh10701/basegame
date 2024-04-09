using UnityEngine;

[CreateAssetMenu(fileName = nameof(SceneRef),
    menuName = nameof(SceneRef))]
public class SceneRef : ScriptableObject
{
    [SerializeField]
    public string m_ScenePath;
}

