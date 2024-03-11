
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(InterButton))]
[CanEditMultipleObjects]
public class InterButtonEditor : ButtonEditor
{
    SerializedProperty _isShowingInter;
    protected override void OnEnable()
    {
        base.OnEnable();
        _isShowingInter = serializedObject.FindProperty("_isShowingInter");


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_isShowingInter);
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
