using _Base.Monetization.Scripts.UI;
using UnityEditor;
using UnityEditor.UI;

namespace _Base.Monetization.Editor
{
    [CustomEditor(typeof(RewardButton))]
    [CanEditMultipleObjects]
    public class RewardButtonEditor : ButtonEditor
    { 
        SerializedProperty _onRewarded;
        SerializedProperty _onRewardFailed;
        protected override void OnEnable()
        {
            base.OnEnable();
            _onRewarded = serializedObject.FindProperty("_onRewarded");
            _onRewardFailed = serializedObject.FindProperty("_onRewardFailed");


        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_onRewarded);
            EditorGUILayout.PropertyField(_onRewardFailed);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}
