using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.Effects;

namespace AbilitySystem.Outcomes
{
    public interface IOutcome
    {
        void Initialize(object origin, GameObject owner);
        void Play(List<GameObject> targets);
        void Play(IEffect effect, GameObject target);

        IOutcome Copy();
    }
}