using System.Collections.Generic;
using HOTG.Abilities.Outcomes;
using UnityEngine;

namespace HOTG.Abilities.Effects
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