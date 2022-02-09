using System.Collections.Generic;
using UnityEngine;
using System;

namespace AbilitySystem.Target
{
    [Serializable]
    public class RectColliderTargetType : ITargetType
    {
        public TargetType Type => TargetType.RECT_COLLIDER;

        [SerializeField]
        private Bounds _bounds = default(Bounds);

        [SerializeField]
        private LayerMask _layer = default(LayerMask);

        public List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer)
        {
            var colliders = new Collider[10];
            var result = new List<Collider>();
            
            LayerMask layer = overrideTargetLayer != -1 ? overrideTargetLayer : _layer;
            Physics.OverlapBoxNonAlloc(owner.TransformPoint(_bounds.center), _bounds.size,
                colliders, owner.rotation, layer);

            if (colliders.Length > 0)
            {                
                foreach(Collider collider in colliders)
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
            Gizmos.DrawWireCube(owner.TransformPoint(_bounds.center) , _bounds.size);            
        }
#endif
    }
}