using System.Collections.Generic;
using UnityEngine;
using System;

namespace AbilitySystem.Target
{
    [Serializable]
    public class RayTargetType : ITargetType
    {
        public TargetType Type => TargetType.RAY;

        [SerializeField]
        private float _distance = 0;

        [SerializeField]
        private LayerMask _layer = default(LayerMask);

        [SerializeField]
        private bool _allTargets = false;

        public List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer)
        {
            var result = new List<Collider>();
            RaycastHit[] raycastHits = null;
            LayerMask layer = overrideTargetLayer != -1 ? overrideTargetLayer :_layer;
            if (_allTargets)
            {
                raycastHits = Physics.RaycastAll(owner.position, owner.TransformDirection(Vector3.forward), _distance, layer);
            } else if (Physics.Raycast(owner.position, owner.TransformDirection(Vector3.forward), out RaycastHit hit, _distance, layer))
            {
                raycastHits = new RaycastHit[1] { hit };
            }            

            if (raycastHits != null && raycastHits.Length > 0)
            {
                foreach (RaycastHit hit in raycastHits)
                {
                    if (hit.collider != null)
                    {
                        result.Add(hit.collider);

                        if (!_allTargets)
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }

#if DEBUG
        public void Draw(Transform owner)
        {
            Debug.DrawRay(owner.position, owner.TransformDirection(Vector3.forward) * _distance, Color.red);
        }
#endif
    }
}