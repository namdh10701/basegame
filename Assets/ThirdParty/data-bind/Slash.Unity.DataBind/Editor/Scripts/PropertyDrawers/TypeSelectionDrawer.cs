// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeSelectionDrawer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.PropertyDrawers
{
    using System.Linq;
    using System.Reflection;

    using Slash.Unity.DataBind.Core.Utils;
    using UnityEditor;

    using UnityEngine;

    [CustomPropertyDrawer(typeof(TypeSelectionAttribute))]
    public class TypeSelectionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();

            TypeSelectionAttribute typeSelectionAttribute = (TypeSelectionAttribute)this.attribute;

            // Find all available classes.
            var types =
                Assembly.GetAssembly(typeof(TypeSelectionAttribute))
                    .GetTypes()
                    .Where(typeSelectionAttribute.BaseType.IsAssignableFrom)
                    .ToList();
            var typeNames = types.Select(type => type.AssemblyQualifiedName);
            var typeIndex = string.IsNullOrEmpty(property.stringValue)
                ? 0
                : types.FindIndex(type => type != null && type.AssemblyQualifiedName == property.stringValue);
            var newContextTypeIndex = EditorGUI.Popup(
                position,
                label.text,
                typeIndex,
                typeNames.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = types[newContextTypeIndex].AssemblyQualifiedName;
            }
            EditorGUI.EndProperty();
        }
    }
}