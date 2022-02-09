using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AbilitySystem.TargetType;

namespace AbilitySystem.Editor
{
    [CustomEditor(typeof(AbilityConfig))]
    public class AbilityConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _targetType;
        private SerializedProperty _targetTypeImpl;
        private SerializedProperty _cooldown;
        private SerializedProperty _timeCastAbility;
        private SerializedProperty _maxOfTargets;
        private SerializedProperty _outcomes;
        private SerializedProperty _effects;

        private ReorderableList _outcomesList;
        private ReorderableList _effectsList;

        private void OnEnable()
        {
            _targetType = serializedObject.FindProperty("_targetType");
            _targetTypeImpl = serializedObject.FindProperty("_targetTypeImpl");
            _cooldown = serializedObject.FindProperty("_cooldown");
            _timeCastAbility = serializedObject.FindProperty("_timeCastAbility");
            _maxOfTargets = serializedObject.FindProperty("_maxOfTargets");
            _outcomes = serializedObject.FindProperty("_outcomes");
            _effects = serializedObject.FindProperty("_effects");

            _outcomesList = new ReorderableList(serializedObject, _outcomes, false, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Outcomes"),
                drawElementCallback = DrawOutcomeCallback,
                elementHeightCallback = index => EditorGUI.GetPropertyHeight(_outcomes.GetArrayElementAtIndex(index)) + 2
            };

            _effectsList = new ReorderableList(serializedObject, _effects, false, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Effets"),
                drawElementCallback = DrawEffectCallback,
                elementHeightCallback = index => EditorGUI.GetPropertyHeight(_effects.GetArrayElementAtIndex(index)) + 2
            };

            SceneView.duringSceneGui += OnSceneGui;

            if (!string.IsNullOrEmpty(_targetTypeImpl.managedReferenceFullTypename) &&
                !AbilityEditorUtils.IsCorrectType(
                (TargetTypeItem)_targetType.enumValueIndex, _targetTypeImpl))
            {
                _targetTypeImpl.managedReferenceValue = AbilityEditorUtils.GetTargetByType((TargetTypeItem)_targetType.enumValueIndex);
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGui;
        }

        private void OnSceneGui(SceneView obj)
        {
            AbilityEditorUtils.DrawTargetByType((TargetTypeItem)_targetType.enumValueIndex, _targetTypeImpl);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawTargetType();

            EditorGUILayout.Space();

            _cooldown.floatValue = EditorGUILayout.Slider("Cooldown", _cooldown.floatValue, 0, 100, GUILayout.ExpandWidth(true));
            _timeCastAbility.floatValue = EditorGUILayout.Slider("Time to cast", _timeCastAbility.floatValue, 0, 100, GUILayout.ExpandWidth(true));
            _maxOfTargets.intValue = EditorGUILayout.IntSlider("Max of targets", _maxOfTargets.intValue, 0, 100, GUILayout.ExpandWidth(true));            

            EditorGUILayout.Space();

            _outcomesList.DoLayoutList();

            EditorGUILayout.Space();

            _effectsList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTargetType()
        {
            EditorGUILayout.Separator();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_targetType);

            if (EditorGUI.EndChangeCheck())
            {
                _targetTypeImpl.managedReferenceValue = AbilityEditorUtils.GetTargetByType((TargetTypeItem)_targetType.enumValueIndex);
            }

            EditorGUILayout.PropertyField(_targetTypeImpl);
        }

        private void DrawOutcomeCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty outcome = _outcomes.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, outcome);
        }

        private void DrawEffectCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty outcome = _effects.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, outcome);
        }
    }
}