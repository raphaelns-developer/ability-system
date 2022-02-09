using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HOTG.Abilities.Effects.Editor
{
    [CustomPropertyDrawer(typeof(Effect), true)]
    public class EffectDrawer : PropertyDrawer
    {
        private ReorderableList _outcomesList;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty canDodge = property.FindPropertyRelative("_canDodge");
            SerializedProperty outcomes = property.FindPropertyRelative("_outcomes");

            if (_outcomesList == null)
            {
                _outcomesList = new ReorderableList(outcomes.serializedObject, outcomes, false, true, true, true)
                {
                    drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Outcomes"),
                    drawElementCallback = DrawElementCallback,
                    elementHeightCallback = index => EditorGUI.GetPropertyHeight(_outcomesList.serializedProperty.GetArrayElementAtIndex(index)) + 2
                };
            }
            
            var fieldRect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight
            };
            EditorGUI.PropertyField(fieldRect, canDodge);
                        
            fieldRect.y += EditorGUIUtility.singleLineHeight + 2;
            fieldRect.height = EditorGUI.GetPropertyHeight(outcomes);
            _outcomesList.DoList(fieldRect);
        }

        private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty outcome = _outcomesList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, outcome);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float outcomeHeight = _outcomesList?.GetHeight() ?? 0;
            
            return EditorGUIUtility.singleLineHeight * 2 + 8 + outcomeHeight;
        }
    }
}