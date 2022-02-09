using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOTG.Abilities.Target
{
    [Serializable]
    public class NoneTargetType : ITargetType
    {
        public TargetType Type => TargetType.NONE;

        public List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer)
        {
            return new List<Collider>();
        }

#if DEBUG
        public void Draw(Transform owner)
        {
            //
        }
#endif
    }
}
