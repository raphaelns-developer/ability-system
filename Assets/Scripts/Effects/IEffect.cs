using System.Collections.Generic;
using AbilitySystem.Outcomes;
using UnityEngine;

namespace AbilitySystem.Effects
{
    public interface IEffect
    {
        object Origin { get; }
        GameObject Owner { get; }
        bool CanBeDodge { get; }
        List<IOutcome> Outcomes { get; }

        void Initialize(object origin, GameObject owner);

        bool Play(GameObject target);

        IEffect Copy();
    }
    
}