using System.Collections.Generic;
using HOTG.CustomAttributes;
using HOTG.Abilities.Effects;
using HOTG.Abilities.Outcomes;
using HOTG.Abilities.Target;
using UnityEngine;

namespace HOTG.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Heroes of The Galaxy/Ability")]
    public class AbilityConfig : ScriptableObject
    {
        public int ID => GetHashCode();

        [SerializeField]
        private TargetType _targetType = TargetType.NONE;
        
        [SerializeReference] 
        private ITargetType _targetTypeImpl = null;
        public ITargetType TargetTypeImpl => _targetTypeImpl;

        [SerializeField]
        private float _cooldown;
        public float Cooldown => _cooldown;
        [SerializeField]
        private float _timeCastAbility;
        public float TimeCastAbility => _timeCastAbility;

        [SerializeField]
        private float _abilityLifeTime = 0;
        public float AbilityLifeTime => _abilityLifeTime;

        [SerializeField]
        private int _maxOfTargets = 0;
        public int MaxOfTargets => _maxOfTargets;

        [SerializeReference]
        [GenericList(typeof(IOutcome))]
        private List<IOutcome> _outcomes = null;
        public List<IOutcome> Outcomes => _outcomes;

        [Space]
        [Space]
        
        [SerializeReference]
        [GenericList(typeof(IEffect))]
        private List<IEffect> _effects = null;

        public List<IEffect> Effects => _effects;
    }
}