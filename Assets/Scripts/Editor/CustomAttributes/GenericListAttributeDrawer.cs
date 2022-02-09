using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HOTG.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(GenericListAttribute), false)]
    public class GenericListAttributeDrawer : SerializeReferenceInspectDrawer
    {
        private SerializedProperty _serializedArray;
        private readonly Dictionary<string, int> _elementIndexes = new Dictionary<string, int>();
                
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
	        int index = GetArrayIndex(property);	        

            if (_serializedArray == null)
            {
                _serializedArray = GetParentArray(property, out index);
            }

            _elementIndexes[property.propertyPath] = index;

            base.OnGUI(position, property, label);
        }

        protected override void DrawHeader(Rect position, SerializedProperty property, string typeName)
        {
            base.DrawHeader(new Rect(position) { width = position.width - 25}, property, typeName);
            var buttonRect = new Rect(position)
            {
                x = position.x + position.width - 25,
                width = 25,
                height = EditorGUIUtility.singleLineHeight + 5
            };

            if (GUI.Button(buttonRect, new GUIContent(EditorGUIUtility.IconContent("d_TreeEditor.Trash"))))
            {
                DeleteItem(property);
                Event.current.Use();
            }
        }        
		
		private void DeleteItem(SerializedProperty property)
		{
			_elementIndexes.TryGetValue(property.propertyPath, out int index);
			_serializedArray.serializedObject.UpdateIfRequiredOrScript();
				
			Undo.RegisterCompleteObjectUndo(_serializedArray.serializedObject.targetObject,
				"Delete element at " + index);
			Undo.FlushUndoRecordObjects();

			property.managedReferenceValue = null;

			_serializedArray.DeleteArrayElementAtIndex(index);
			_serializedArray.serializedObject.ApplyModifiedProperties();

			_serializedArray = null;
		}
		
		private static SerializedProperty GetParentArray(SerializedProperty element, out int index)
        {
            index = GetArrayIndex(element);
            if (index < 0)
            {
                return null;
            }

            string propertyPath = element.propertyPath;

            string[] fullPathSplit = propertyPath.Split('.');

            string pathToArray = string.Empty;
            for(int i = 0; i < fullPathSplit.Length - 2; i++)
            {
                pathToArray = i < fullPathSplit.Length - 3
                    ? string.Concat(pathToArray, fullPathSplit[i], ".")
                    : string.Concat(pathToArray, fullPathSplit[i]);
            }

            return element.serializedObject.FindProperty(pathToArray);
        }
        
        private static int GetArrayIndex(SerializedProperty element)
        {
            string propertyPath = element.propertyPath;
            if (!propertyPath.Contains(".Array.data[") || !propertyPath.EndsWith("]"))
            {
                return -1;
            }

            int start = propertyPath.LastIndexOf("[", StringComparison.Ordinal);
            var str = propertyPath.Substring(start + 1, propertyPath.Length - start - 2);
            int.TryParse(str, out var index);
            
            return index;
        }
    }
}