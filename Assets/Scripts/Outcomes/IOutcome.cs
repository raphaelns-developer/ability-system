using System.Collections.Generic;
using UnityEngine;
using HOTG.Abilities.Effects;

namespace HOTG.Abilities.Outcomes
{
    public interface IOutcome
    {
        void Initialize(object origin, GameObject owner);
        void Play(List<GameObject> targets);
        void Play(IEffect effect, GameObject target);

        IOutcome Copy();
    }
}