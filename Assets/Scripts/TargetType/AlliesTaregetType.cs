using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOTG.Abilities.Target
{
    [Serializable]
    public class AlliesTargetType : ITargetType
    {
        public TargetType Type => TargetType.ALLIES;

        [SerializeField]
        private bool _includeOwner;

        public List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer)
        {
            // Find a way to get all allies in the game (* not use FindAllObjectsOfType)
            return null;
        }

#if DEBUG
        public void Draw(Transform owner)
        {
            //
        }
#endif
    }
}