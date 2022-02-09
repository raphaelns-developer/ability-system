using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.Target
{
    [Serializable]
    public class AnyTargetType : ITargetType
    {
        public TargetType Type => TargetType.ANY;

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
