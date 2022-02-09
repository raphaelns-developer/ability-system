using System;
using System.Collections.Generic;
using HOTG.CustomAttributes;
using HOTG.Abilities.Outcomes;
using UnityEngine;

namespace HOTG.Abilities.Effects
{
    [Serializable]
    public abstract class Effect
    {
        public int Id { get; protected set; }
        public object Origin { get; protected set; }
        public GameObject Owner { get; protected set; }        

        [SerializeField]
        protected bool _canDodge = false;

        public bool CanBeDodge
        {
            get
            {
                return _canDodge;
            }
        }

        [SerializeReference]
        [GenericList(typeof(IOutcome))]
        protected List<IOutcome> _outcomes = new List<IOutcome>();
        public List<IOutcome> Outcomes => _outcomes;

        public virtual void Initialize(object origin, GameObject owner)
        {
            Origin = origin;
            Owner = owner;

            foreach (IOutcome outcome in _outcomes)
            {
                outcome.Initialize(origin, owner);
            }
        }

        public abstract bool Play(GameObject target);

        public abstract IEffect Copy();
    }
}