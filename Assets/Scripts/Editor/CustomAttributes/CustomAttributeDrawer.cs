using UnityEditor;
using UnityEngine;

namespace HOTG.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(CustomAttributeAttribute))]
    public class CustomAttributeDrawer : PropertyDrawer
    {
        private bool _isOpen = false;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CustomAttributeAttribute attr = attribute as CustomAttributeAttribute;
            
            SerializedProperty attributeType = property.FindPropertyRelative("_attributeType");
            SerializedProperty value = property.FindPropertyRelative("_value");
            SerializedProperty currentValue = property.FindPropertyRelative("_currentValue");
            SerializedProperty onValueUpdate = property.FindPropertyRelative("_onValueUpdate");

            string name = string.IsNullOrEmpty(attr?.Name)
                ? attr?.Name
                : attributeType.enumNames[attributeType.enumValueIndex];

            EditorGUI.BeginProperty(position, label, property);
            
            GUIStyle foldOutStyle = new GUIStyle(GUI.skin.box)
            {
                fixedWidth = 0, 
                stretchWidth = true, 
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold
            };

            var fieldsRect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight + 5
            };

            if (GUI.Button(fieldsRect, name, foldOutStyle))
            {
                _isOpen = !_isOpen;
            }

            if (!_isOpen)
            {
                return;
            }
            
            fieldsRect.height = EditorGUIUtility.singleLineHeight;
            fieldsRect.y += fieldsRect.height + 8;
            EditorGUI.PropertyField(fieldsRect, attributeType);
            
            fieldsRect.y += fieldsRect.height + 2;
            EditorGUI.Slider(fieldsRect, value, 0, 100);
            
            fieldsRect.y += fieldsRect.height + 2;
            GUI.enabled = false;
            EditorGUI.FloatField(fieldsRect, "Current Value", currentValue.floatValue);
            GUI.enabled = true;
            
            fieldsRect.y += fieldsRect.height + 2;
            EditorGUI.PropertyField(fieldsRect, onValueUpdate);
            
            // Set the current value
            if (!Application.isPlaying)
            {
                currentValue.floatValue = value.floatValue;
            }
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * (_isOpen ?  11 : 1);
        }
    }
}