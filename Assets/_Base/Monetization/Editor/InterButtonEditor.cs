using _Base.Monetization.Scripts.UI;
using UnityEditor;
using UnityEditor.UI;

namespace _Base.Monetization.Editor
{
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
}
