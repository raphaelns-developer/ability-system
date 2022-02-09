using UnityEngine;
using UnityEditor;
using AbilitySystem.TargetType;
using System;

namespace AbilitySystem.Outcomes.Editor
{
    [CustomPropertyDrawer(typeof(RectColliderTargetType), true)]
    public class RectColliderTargetTypeEditor : PropertyDrawer
    { 

        public override void OnGUI(Rect position, SerializedProperty property,
                GUIContent label)
        {
            SerializedProperty bounds = property.FindPropertyRelative("_bounds");
            SerializedProperty layer = property.FindPropertyRelative("_layer");

            var rect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight * 3
            };

            EditorGUI.PropertyField(position, bounds);

            rect.y += 6 + rect.height;
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, layer);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 5;
        }
    }
}

    
