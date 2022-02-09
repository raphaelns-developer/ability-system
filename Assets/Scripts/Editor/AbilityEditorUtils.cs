using AbilitySystem.TargetType;
using UnityEngine;
using UnityEditor;
using HOTG.CustomAttributes.Editor;
using System;

namespace AbilitySystem.Editor
{
    public class AbilityEditorUtils
    {
        public static ITargetType GetTargetByType(TargetTypeItem type)
        {
            switch (type)
            {
                case TargetTypeItem.NONE:
                    return new NoneTargetType();
                case TargetTypeItem.ANY:
                    return new AnyTargetType();
                case TargetTypeItem.ALLIES:
                    return new AlliesTargetType();
                case TargetTypeItem.ENEMIES:
                    return new EnemiesTargetType();
                case TargetTypeItem.RECT_COLLIDER:
                    return new RectColliderTargetType();
                case TargetTypeItem.SPHERE_COLLIDER:
                    return new SphereColliderTargetType();
                case TargetTypeItem.RAY:
                    return new RayTargetType();
                default:
                    return null;
            }
        }

        public static bool IsCorrectType(TargetTypeItem type, SerializedProperty property)
        {
            Type classType = EditorGUIUtils.GetManagedReferenceType(property);
            switch (type)
            {
                case TargetTypeItem.NONE:
                    return classType.IsAssignableFrom(typeof(NoneTargetType));
                case TargetTypeItem.ANY:
                    return classType.IsAssignableFrom(typeof(AnyTargetType));
                case TargetTypeItem.ALLIES:
                    return classType.IsAssignableFrom(typeof(AlliesTargetType));
                case TargetTypeItem.ENEMIES:
                    return classType.IsAssignableFrom(typeof(EnemiesTargetType));
                case TargetTypeItem.RECT_COLLIDER:
                    return classType.IsAssignableFrom(typeof(RectColliderTargetType));
                case TargetTypeItem.SPHERE_COLLIDER:
                    return classType.IsAssignableFrom(typeof(SphereColliderTargetType));
                case TargetTypeItem.RAY:
                    return classType.IsAssignableFrom(typeof(RayTargetType));
                default:
                    return false;
            }
        }

        public static void DrawTargetByType(TargetTypeItem type, SerializedProperty property)
        {
            switch (type)
            {
                case TargetTypeItem.RECT_COLLIDER:
                    SerializedProperty bounds = property.FindPropertyRelative("_bounds");
                    Handles.DrawWireCube(bounds.boundsValue.center, bounds.boundsValue.size);
                    break;
                case TargetTypeItem.SPHERE_COLLIDER:
                    SerializedProperty radius = property.FindPropertyRelative("_radius");
                    //Handles.SphereCap(0 ,Vector3.zero, Quaternion.identity, radius.floatValue * 2);
                    break;
                case TargetTypeItem.RAY:
                    SerializedProperty distance = property.FindPropertyRelative("_distance");
                    Handles.DrawLine(Vector3.zero, Vector3.forward * distance.floatValue);
                    break;
            }
        }
    }
}

    
