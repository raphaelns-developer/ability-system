using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOTG.Abilities.Target
{
    [Serializable]
    public class SphereColliderTargetType : ITargetType
    {
        public TargetType Type => TargetType.RECT_COLLIDER;

        [SerializeField]
        private float _radius;

        [SerializeField]
        private LayerMask _layer;

        public List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer)
        {
            var colliders = new Collider[10];
            var result = new List<Collider>();
            LayerMask layer = overrideTargetLayer != -1 ? overrideTargetLayer : _layer;
            Physics.OverlapSphereNonAlloc(owner.position, _radius, colliders, layer);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider != null && collider.gameObject != null)
                    {
                        result.Add(collider);
                    }
                }
            }

            return result;
        }

#if DEBUG
        public void Draw(Transform owner)
        {
            Gizmos.DrawSphere(owner.position, _radius);
        }
#endif
    }
}