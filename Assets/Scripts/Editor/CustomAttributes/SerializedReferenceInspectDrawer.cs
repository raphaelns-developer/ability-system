using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HOTG.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(SerializeReferenceInspect), false)]
    public class SerializeReferenceInspectDrawer : PropertyDrawer
    {
        private SerializeReferenceInspect _attribute;
        private float _propertyHeight;

        private static GUIStyle _headerStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_headerStyle == null)
            {
                _headerStyle = new GUIStyle(GUI.skin.box)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold
                };
            }

            _attribute = attribute as SerializeReferenceInspect;
            var typeName = EditorGUIUtils.GetClassName(property.managedReferenceFullTypename);

            DrawHeader(position, property, typeName);

            if (!property.isExpanded)
            {
                _propertyHeight = EditorGUIUtility.singleLineHeight + 5;
                return;
            }

            Type propertyType = EditorGUIUtils.GetManagedReferenceType(property);
            if (propertyType == null) return;

            PropertyDrawer drawer = EditorGUIUtils.GetDefaultDrawer(propertyType, fieldInfo, attribute);

            var propertyRect = new Rect(position)
            {
                y = position.y + EditorGUIUtility.singleLineHeight + 4,
                height = _propertyHeight
            };

            if (Event.current.type == EventType.Repaint)
            {
                GUI.skin.window.Draw(propertyRect, GUIContent.none, 0);
            }

            propertyRect.y += 2;
            propertyRect.x += 4;
            propertyRect.width -= 8;
            if (drawer != null)
            {
                drawer.OnGUI(propertyRect, property, label);
                _propertyHeight = drawer.GetPropertyHeight(property, label);
            }
            else
            {
                _propertyHeight = DrawDefaultProperty(propertyRect, property);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {            
            return _propertyHeight + (property.isExpanded ? EditorGUIUtility.singleLineHeight + 2 : 4);
        }

        protected virtual void DrawHeader(Rect position, SerializedProperty property, string typeName)
        {
            var buttonRect = new Rect(position)
            {
                width = position.width - 25,
                height = EditorGUIUtility.singleLineHeight + 5
            };
            if (GUI.Button(buttonRect, typeName, _headerStyle))
            {
                property.isExpanded = !property.isExpanded;
            }

            buttonRect.x = position.x + buttonRect.width;
            buttonRect.width = 25;
            if (GUI.Button(buttonRect, new GUIContent(EditorGUIUtility.IconContent("d_icon dropdown@2x"))))
            {
                ShowMenu(property.Copy(), true);
                Event.current.Use();
            }
        }

        protected virtual float DrawDefaultProperty(Rect position, SerializedProperty property)
        {
            float height = 10;
            var childPos = new Rect(position) { height = EditorGUIUtility.singleLineHeight };

            if (!property.hasVisibleChildren ||!property.NextVisible(true)) return height * 2;
            int depth = property.depth;
            do
            {
                height += EditorGUI.GetPropertyHeight(property);

                if (property.isArray)
                {
                    var arrayProperty = property.Copy();
                    var arrayRect = new Rect(childPos)
                    {
                        x = childPos.x + 14,
                        width = childPos.width - 18
                    };

                    do
                    {
                        EditorGUI.PropertyField(new Rect(arrayRect), arrayProperty);
                        arrayRect.y += arrayRect.height;
                    } while (arrayProperty.NextVisible(true) && arrayProperty.depth > depth);

                    childPos.y = arrayRect.y + 2;
                    height += childPos.height;
                }
                else
                {
                    EditorGUI.PropertyField(childPos, property);
                    childPos.y += childPos.height + 1;
                }      
            } while (property.NextVisible(false) && property.depth == depth);
            
            return height;
        }

        protected virtual void ShowMenu(SerializedProperty property, bool applyArray)
        {
            GenericMenu context = new GenericMenu();

            var types = _attribute.Types;
            if (types != null)
            {
                foreach (SerializeReferenceInspect.TypeInfo type in types)
                {
                    context.AddItem(new GUIContent(type.Path), false, OnAdd, type.Path);
                }
            }

            context.ShowAsContext();

            void OnAdd(object path)
            {
                var typeInfo = _attribute.TypeInfoByPath(path as string);
                if (typeInfo == null)
                {
                    Debug.LogError($"Type '{path}' not found.");
                    return;
                }

                Undo.RegisterCompleteObjectUndo(property.serializedObject.targetObject, $"Create instance of {typeInfo.Type}");
                Undo.FlushUndoRecordObjects();

                var instance = Activator.CreateInstance(typeInfo.Type);
                property.managedReferenceValue = instance;
                property.isExpanded = true;
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}