﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataBindingPropertyDrawer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.PropertyDrawers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    ///     Property Drawer for <see cref="DataBinding" />.
    /// </summary>
    [CustomPropertyDrawer(typeof(DataBinding))]
    public class DataBindingPropertyDrawer : PropertyDrawer
    {
        private static float LineHeight { get { return EditorGUIUtility.singleLineHeight; } }

        private static float LineSpacing { get { return EditorGUIUtility.standardVerticalSpacing; } }

        private Rect selectGameObjectComponentRect;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                return LineHeight;
            }

            var targetTypeProperty = property.FindPropertyRelative("Type");
            var providerProperty = property.FindPropertyRelative("Provider");
            var constantProperty = property.FindPropertyRelative("Constant");
            var referenceProperty = property.FindPropertyRelative("Reference");
            var pathProperty = property.FindPropertyRelative("Path");

            var targetType = (DataBindingType)targetTypeProperty.enumValueIndex;
            float targetTypeHeight = 0;
            switch (targetType)
            {
                case DataBindingType.Context:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(pathProperty);
                    break;
                case DataBindingType.Provider:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(providerProperty);
                    break;
                case DataBindingType.Constant:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(constantProperty);
                    break;
                case DataBindingType.Reference:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(referenceProperty);
                    break;
            }

            return LineHeight + targetTypeHeight + LineSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                EditorGUI.LabelField(position, "Multi-object editing not supported for data bindings");
                return;
            }

            var targetTypeProperty = property.FindPropertyRelative("Type");
            var providerProperty = property.FindPropertyRelative("Provider");
            var constantProperty = property.FindPropertyRelative("Constant");
            var referenceProperty = property.FindPropertyRelative("Reference");
            var pathProperty = property.FindPropertyRelative("Path");

            label = EditorGUI.BeginProperty(position, label, property);
            var contentPosition = EditorGUI.PrefixLabel(position, label);
            if (targetTypeProperty != null)
            {
                // Type selection.
                contentPosition.height = LineHeight;
                EditorGUI.PropertyField(contentPosition, targetTypeProperty, GUIContent.none);
                position.y += LineHeight + LineSpacing;

                // Check for type hint.
                var typeHintAttributes =
                    this.fieldInfo.GetCustomAttributes(typeof(DataTypeHintAttribute), true);
                var typeHintAttribute = (DataTypeHintAttribute)typeHintAttributes.FirstOrDefault();
                var typeHint = typeHintAttribute != null
                    ? typeHintAttribute.GetTypeHint(this.fieldInfo.DeclaringType)
                    : null;

                // Draw type specific fields.
                EditorGUI.indentLevel++;
                var targetType = (DataBindingType)targetTypeProperty.enumValueIndex;
                switch (targetType)
                {
                    case DataBindingType.Context:
                        {
                            var rect = new Rect(position) { height = LineHeight };
                            EditorGUI.PropertyField(rect, pathProperty);
                        }
                        break;
                    case DataBindingType.Provider:
                        {
                            var rect = new Rect(position) { height = LineHeight };
                            var dataProviderType = typeof(IDataProvider);
                            GameObjectComponentSelectionField(rect, new GUIContent("Provider"), providerProperty,
                                typeof(Object),
                                component =>
                                {
                                    var componentType = component.GetType();
                                    if (!dataProviderType.IsAssignableFrom(componentType))
                                    {
                                        return false;
                                    }

                                    if (typeHint != null)
                                    {
                                        var providerDataTypeAttribute = (DataTypeHintAttribute)componentType
                                            .GetCustomAttributes(typeof(DataTypeHintAttribute), true).FirstOrDefault();
                                        if (providerDataTypeAttribute != null)
                                        {
                                            var providerTypeHint = providerDataTypeAttribute.GetTypeHint(componentType);
                                            if (!typeHint.IsAssignableFrom(providerTypeHint))
                                            {
                                                return false;
                                            }
                                        }
                                    }

                                    return true;
                                },
                                ref this.selectGameObjectComponentRect);
                        }
                        break;
                    case DataBindingType.Constant:
                        {
                            var rect = new Rect(position) { height = LineHeight };
                            EditorGUI.PropertyField(rect, constantProperty, new GUIContent("Constant"));
                        }
                        break;
                    case DataBindingType.Reference:
                        {
                            var rect = new Rect(position) { height = LineHeight };
                            GameObjectComponentSelectionField(rect, new GUIContent("Reference"), referenceProperty,
                                typeHint, null,
                                ref this.selectGameObjectComponentRect);
                        }
                        break;
                }
            }

            --EditorGUI.indentLevel;

            EditorGUI.EndProperty();
        }

        private static void GameObjectComponentSelectionField(Rect rect, GUIContent label,
            SerializedProperty serializedProperty,
            Type objectType,
            Func<Component, bool> componentFilter,
            ref Rect popupWindowRect)
        {
            if (objectType == null)
            {
                objectType = typeof(Object);
            }

            var reference = serializedProperty.objectReferenceValue;
            var newReference = EditorGUI.ObjectField(rect, label, reference, objectType, true);
            if (newReference != reference)
            {
                // If a game object was dragged, let user select from all possible components
                var gameObjectReference = DragAndDrop.objectReferences.Length > 0
                    ? DragAndDrop.objectReferences[0] as GameObject
                    : null;
                if (gameObjectReference != null)
                {
                    // Check allowed components on game object.
                    var allComponents = gameObjectReference.GetComponents<Component>();

                    // Filter components.
                    var allowedComponents = allComponents.Where(objectType.IsInstanceOfType)
                        .Where(componentFilter ?? (c => true)).ToList();

                    // Check if only one object works. If so, choose it automatically.
                    if (allowedComponents.Count == 0)
                    {
                        if (objectType.IsInstanceOfType(gameObjectReference))
                        {
                            serializedProperty.objectReferenceValue = gameObjectReference;
                        }
                        else
                        {
                            Debug.LogWarning("Dragged game object doesn't have a fitting component");
                        }
                    }
                    else if (allowedComponents.Count == 1)
                    {
                        serializedProperty.objectReferenceValue = allowedComponents[0];
                    }
                    else
                    {
                        // Let user select component reference if he wants to.
                        PopupWindow.Show(popupWindowRect,
                            new SelectGameObjectComponentPopupWindowContent(
                                objectType.IsInstanceOfType(gameObjectReference) ? gameObjectReference : null,
                                allowedComponents,
                                selectedReference =>
                                {
                                    serializedProperty.objectReferenceValue = selectedReference;
                                    serializedProperty.serializedObject.ApplyModifiedProperties();
                                }));
                    }
                }
                else
                {
                    serializedProperty.objectReferenceValue = newReference;
                }
            }

            if (Event.current.type == EventType.Repaint)
            {
                popupWindowRect = GUILayoutUtility.GetLastRect();
            }
        }

        private class SelectGameObjectComponentPopupWindowContent : PopupWindowContent
        {
            private readonly IList<Component> allowedComponents;

            private readonly Action<Object> callback;

            private readonly GameObject gameObject;

            public SelectGameObjectComponentPopupWindowContent(GameObject gameObject,
                IList<Component> allowedComponents, Action<Object> callback)
            {
                this.gameObject = gameObject;
                this.callback = callback;
                this.allowedComponents = allowedComponents;
            }

            public override void OnGUI(Rect rect)
            {
                if (this.gameObject != null && GUILayout.Button("Use Game Object", GUILayout.Width(200)))
                {
                    this.callback(this.gameObject);
                    this.editorWindow.Close();
                }

                if (this.allowedComponents.Count > 0)
                {
                    EditorGUILayout.LabelField("Component:", EditorStyles.boldLabel);
                    foreach (var component in this.allowedComponents)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(component.GetType().Name, GUILayout.Width(100));
                        if (GUILayout.Button("Choose", GUILayout.Width(100)))
                        {
                            this.callback(component);
                            this.editorWindow.Close();
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}