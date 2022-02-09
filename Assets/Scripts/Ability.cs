using System.Collections.Generic;
using AbilitySystem.Outcomes;
using AbilitySystem.Effects;
using AbilitySystem.TargetType;
using UnityEngine;

namespace AbilitySystem
{
    public class Ability
    {
        public int ConfigId { get; private set; }

        private readonly ITargetType _targetType;

        private float _cooldown;
        public float Cooldown { get => _cooldown; }

        private float _timeCastAbility;
        public float TimeCastAbility { get => _timeCastAbility; }

        private readonly int _maxOfTargets;

        private readonly List<IOutcome> _outcomes;
        private readonly List<IEffect> _effects;

        private readonly Transform _ownerTransform;  
        private LayerMask _overrideTargetLayer;
        
#if UNITY_EDITOR
        public List<IOutcome> Outcomes => _outcomes;
        public List<IEffect> Effects => _effects;
#endif

        public Ability(int id, ITargetType targetType, float cooldown, float timeCastAbility, int maxTargets, List<IOutcome> outcomes, List<IEffect> effects, GameObject owner, object origin)
        {
            ConfigId = id;
            _targetType = targetType;
            _cooldown = cooldown;
            _timeCastAbility = timeCastAbility;
            _maxOfTargets = maxTargets;
            _outcomes = outcomes;
            _effects = effects;

            _ownerTransform = owner.GetComponent<Transform>();

            foreach (IEffect effect in _effects)
            {
                effect.Initialize(origin, owner);
            }
            
            foreach (IOutcome outcome in _outcomes)
            {
                outcome.Initialize(origin, owner);
            }

            _overrideTargetLayer = -1;
        }

        public void CheckForTargets()
        {
            // Play abilities outcomes
            TriggerOutcomes();

            List<Collider> targets = _targetType?.GetTargets(_ownerTransform, _overrideTargetLayer);
            if (targets == null) return;

            if (_maxOfTargets > 0 && targets.Count > _maxOfTargets)
            {
                targets = targets.GetRange(0, _maxOfTargets);
            }

            var objects = new List<GameObject>(targets.Count);
            foreach (Collider collider in targets)
            {
                objects.Add(collider.gameObject);
            }

            TriggerEffects(objects);     
        }

        public void TriggerOutcomes()
        {
            foreach (IOutcome outcome in _outcomes)
            {
                outcome.Play(null);
            }
        }

        public void TriggerEffects(List<GameObject> targets)
        {
            foreach (IEffect effect in _effects)
            {
                foreach (IOutcome outcome in effect.Outcomes)
                {
                    outcome.Play(targets);
                }

                foreach (GameObject target in targets)
                {
                    if (!effect.Play(target)) continue;

                    if (effect.Outcomes != null)
                    {
                        foreach (IOutcome outcome in effect.Outcomes)
                        {
                            outcome.Play(effect, target);
                        }
                    }
                }
            }              
        }

        public void OverrideTargetlayer(LayerMask targetLayer)
        {
            _overrideTargetLayer = targetLayer;
        }

        public static Ability FromConfig(AbilityConfig config, GameObject owner, object origin)
        {
            var outcomes = new List<IOutcome>();

            if (config.Outcomes != null)
            {
                foreach (IOutcome outcome in config.Outcomes)
                {
                    outcomes.Add(outcome.Copy());
                }
            }
            
            var effects = new List<IEffect>();

            if (config.Effects != null)
            {
                foreach (IEffect effect in config?.Effects)
                {
                    effects.Add(effect.Copy());
                }
            }
            
            return new Ability(config.ID, config.TargetTypeImpl, config.Cooldown, config.TimeCastAbility, config.MaxOfTargets, outcomes, effects, owner, origin);
        }
    }
}