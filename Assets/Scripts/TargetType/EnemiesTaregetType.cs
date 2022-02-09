using System.Collections.Generic;
using UnityEngine;
using System;

namespace AbilitySystem.Target
{
    [Serializable]
    public class EnemiesTargetType : ITargetType
    {
        public TargetType Type => TargetType.ENEMIES;

        public List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer)
        {
            // Find a way to get all enemies in the game (* not use FindAllObjectsOfType)
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