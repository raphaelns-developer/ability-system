using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TargetType
{
    [Serializable]
    public class AnyTargetType : ITargetType
    {
        public TargetTypeItem Type => TargetTypeItem.ANY;

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
