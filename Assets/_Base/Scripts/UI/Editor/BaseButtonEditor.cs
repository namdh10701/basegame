using _Base.Scripts.UI.Buttons;
using UnityEditor;
using UnityEditor.UI;

namespace _Base.Scripts.UI.Editor
{
    [CustomEditor(typeof(BaseButton))]
    [CanEditMultipleObjects]
    public class BaseButtonEditor : ButtonEditor
    {
        SerializedProperty _isSpamable;
        SerializedProperty _allowOnlyTap;

        protected override void OnEnable()
        {
            base.OnEnable();
            _isSpamable = serializedObject.FindProperty("_isSpamable");
            _allowOnlyTap = serializedObject.FindProperty("_allowOnlyTap");
        

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_isSpamable);
            EditorGUILayout.PropertyField(_allowOnlyTap);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}

