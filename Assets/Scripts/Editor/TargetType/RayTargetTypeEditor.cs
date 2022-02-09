using UnityEngine;
using UnityEditor;
using AbilitySystem.Target;

namespace AbilitySystem.Outcomes.Editor
{
    [CustomPropertyDrawer(typeof(RayTargetType), true)]
    public class RayTargetTypeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
                GUIContent label)
        {
            SerializedProperty distance = property.FindPropertyRelative("_distance");
            SerializedProperty layer = property.FindPropertyRelative("_layer");
            SerializedProperty allTargets = property.FindPropertyRelative("_allTargets");

            var rect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight
            };

            distance.floatValue = EditorGUI.Slider(rect, "Distance", distance.floatValue, 0, 100);

            rect.y += 6 + rect.height;
            EditorGUI.PropertyField(rect, layer);

            rect.y += 6 + rect.height;
            EditorGUI.PropertyField(rect, allTargets);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4;
        }
    }
}