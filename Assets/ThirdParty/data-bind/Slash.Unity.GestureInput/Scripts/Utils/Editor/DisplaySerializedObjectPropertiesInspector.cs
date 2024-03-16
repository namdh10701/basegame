using UnityEditor;
using UnityEngine;

namespace Slash.Unity.GestureInput.Utils.Editor
{
    [CustomPropertyDrawer(typeof (DisplaySerializedObjectPropertiesAttribute))]
    public class DisplaySerializedObjectPropertiesInspector : PropertyDrawer
    {
        private float drawerHeight;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var e = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
            var indent = EditorGUI.indentLevel;
            var buttonRect = new Rect(position.x, position.y, 20, 20);
            if (property.isExpanded)
            {
                if (GUI.Button(buttonRect, "-"))
                {
                    property.isExpanded = false;
                }
            }
            else
            {
                if (GUI.Button(buttonRect, "+"))
                {
                    property.isExpanded = true;
                }
            }
            this.drawerHeight = 16;
            position.x += 5;
            position.height = 16;
            EditorGUI.PropertyField(position, property);
            position.y += 24;
            if (!property.isExpanded)
            {
                return;
            }
            if (e != null)
            {
                position.x += 24;
                position.width -= 40;
                var so = e.serializedObject;
                so.Update();
                var prop = so.GetIterator();
                
                prop.NextVisible(true);
                var depthChilden = 0;
                var showChildren = false;
                while (prop.NextVisible(true))
                {
                    if (prop.depth == 0)
                    {
                        showChildren = false;
                        depthChilden = 0;
                    }
                    if (showChildren && prop.depth > depthChilden)
                    {
                        continue;
                    }
                    position.height = 16;
                    EditorGUI.indentLevel = indent + prop.depth;
                    if (EditorGUI.PropertyField(position, prop))
                    {
                        showChildren = false;
                    }
                    else
                    {
                        showChildren = true;
                        depthChilden = prop.depth;
                    }
                    position.y += 20;
                    this.drawerHeight += 20;
                }

                if (GUI.changed)
                {
                    so.ApplyModifiedProperties();
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);
            height += this.drawerHeight;
            return height;
        }
    }
}