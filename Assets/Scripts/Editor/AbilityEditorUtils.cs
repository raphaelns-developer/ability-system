using HOTG.Abilities.Target;
using UnityEngine;
using UnityEditor;
using HOTG.CustomAttributes.Editor;
using System;

namespace HOTG.Abilities.Editor
{
    public class AbilityEditorUtils
    {
        public static ITargetType GetTargetByType(TargetType type)
        {
            switch (type)
            {
                case TargetType.NONE:
                    return new NoneTargetType();
                case TargetType.ANY:
                    return new AnyTargetType();
                case TargetType.ALLIES:
                    return new AlliesTargetType();
                case TargetType.ENEMIES:
                    return new EnemiesTargetType();
                case TargetType.RECT_COLLIDER:
                    return new RectColliderTargetType();
                case TargetType.SPHERE_COLLIDER:
                    return new SphereColliderTargetType();
                case TargetType.RAY:
                    return new RayTargetType();
                default:
                    return null;
            }
        }

        public static bool IsCorrectType(TargetType type, SerializedProperty property)
        {
            Type classType = EditorGUIUtils.GetManagedReferenceType(property);
            switch (type)
            {
                case TargetType.NONE:
                    return classType.IsAssignableFrom(typeof(NoneTargetType));
                case TargetType.ANY:
                    return classType.IsAssignableFrom(typeof(AnyTargetType));
                case TargetType.ALLIES:
                    return classType.IsAssignableFrom(typeof(AlliesTargetType));
                case TargetType.ENEMIES:
                    return classType.IsAssignableFrom(typeof(EnemiesTargetType));
                case TargetType.RECT_COLLIDER:
                    return classType.IsAssignableFrom(typeof(RectColliderTargetType));
                case TargetType.SPHERE_COLLIDER:
                    return classType.IsAssignableFrom(typeof(SphereColliderTargetType));
                case TargetType.RAY:
                    return classType.IsAssignableFrom(typeof(RayTargetType));
                default:
                    return false;
            }
        }

        public static void DrawTargetByType(TargetType type, SerializedProperty property)
        {
            switch (type)
            {
                case TargetType.RECT_COLLIDER:
                    SerializedProperty bounds = property.FindPropertyRelative("_bounds");
                    Handles.DrawWireCube(bounds.boundsValue.center, bounds.boundsValue.size);
                    break;
                case TargetType.SPHERE_COLLIDER:
                    SerializedProperty radius = property.FindPropertyRelative("_radius");
                    //Handles.SphereCap(0 ,Vector3.zero, Quaternion.identity, radius.floatValue * 2);
                    break;
                case TargetType.RAY:
                    SerializedProperty distance = property.FindPropertyRelative("_distance");
                    Handles.DrawLine(Vector3.zero, Vector3.forward * distance.floatValue);
                    break;
            }
        }
    }
}

    
