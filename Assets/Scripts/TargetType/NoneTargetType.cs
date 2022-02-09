using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TargetType
{
    [Serializable]
    public class NoneTargetType : ITargetType
    {
        public TargetTypeItem Type => TargetTypeItem.NONE;

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
