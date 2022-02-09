using UnityEngine;
using UnityEditor;
using AbilitySystem.Target;

namespace AbilitySystem.Outcomes.Editor
{
    [CustomPropertyDrawer(typeof(SphereColliderTargetType), true)]
    public class SphereColliderTargetTypeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
                GUIContent label)
        {
            SerializedProperty radius = property.FindPropertyRelative("_radius");
            SerializedProperty layer = property.FindPropertyRelative("_layer");

            var rect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight
            };

            radius.floatValue = EditorGUI.Slider(rect, "Radius", radius.floatValue, 0, 10);

            rect.y += 6 + rect.height;
            EditorGUI.PropertyField(rect, layer);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }
}