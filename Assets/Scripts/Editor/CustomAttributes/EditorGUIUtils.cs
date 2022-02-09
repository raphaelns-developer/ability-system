using System;
using System.Reflection;
using UnityEditor;

namespace HOTG.CustomAttributes.Editor
{
    public static class EditorGUIUtils
    {
        private static Type _attributeUtilityType;

        public static Type GetManagedReferenceType(SerializedProperty property)
        {
            if (property == null || string.IsNullOrEmpty(property.managedReferenceFieldTypename)) return null;

            string typeName = GetTypeName(property.managedReferenceFullTypename);
            return Type.GetType($"{typeName}, com.HOTG.heroesofthegalaxy");
        }
        
        public static string GetTypeName(string typeName)
        {
            if(string.IsNullOrEmpty(typeName)) return "(empty)";

            var index = typeName.LastIndexOf(' ');
            if(index >= 0) return typeName.Substring(index + 1);

            index = typeName.LastIndexOf('.');
            if(index >= 0) return typeName.Substring(index + 1);

            return typeName;
        }
        
        public static string GetClassName(string typeName)
        {
            typeName = GetTypeName(typeName);
            if (!string.IsNullOrEmpty(typeName))
            {
                int lastIndex = typeName.LastIndexOf(".", StringComparison.CurrentCulture);

                if (lastIndex > 0)
                {
                    typeName = typeName.Substring(lastIndex + 1);    
                }
            }

            return typeName;
        }

        public static PropertyDrawer GetDefaultDrawer(Type type, FieldInfo fieldInfo, Attribute attribute)
        {
            PropertyDrawer drawer = null;
            Type propertyDrawer = GetPropertyDrawerType(type);
            if (propertyDrawer != null)
            {
                drawer = (PropertyDrawer) System.Activator.CreateInstance(propertyDrawer);
                propertyDrawer.GetField("m_FieldInfo", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(drawer, fieldInfo);
                propertyDrawer.GetField("m_Attribute", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(drawer, attribute);
            }

            return drawer;
        }
        
        /// <summary>
        /// Use Reflection to access ScriptAttributeUtility to find the
        /// PropertyDrawer type for a property type
        /// </summary>
        public static Type GetPropertyDrawerType(Type type)
        {
            var scriptAttributeUtilityType = GetScriptAttributeUtilityType();
 
            var getDrawerTypeForTypeMethod =
                scriptAttributeUtilityType.GetMethod(
                    "GetDrawerTypeForType",
                    BindingFlags.Static | BindingFlags.NonPublic, null,
                    new[] { typeof(Type) }, null);
 
            return (Type) getDrawerTypeForTypeMethod.Invoke(null, new[] { type });
        }
        
        private static Type GetScriptAttributeUtilityType()
        {
            if (_attributeUtilityType == null)
            {
                var asm = Array.Find(AppDomain.CurrentDomain.GetAssemblies(),
                    (a) => a.GetName().Name == "UnityEditor.CoreModule");
 
                var types = asm.GetTypes();
                _attributeUtilityType = Array.Find(types, (t) => t.Name == "ScriptAttributeUtility");
            }
 
            return _attributeUtilityType;
        }
    }
}