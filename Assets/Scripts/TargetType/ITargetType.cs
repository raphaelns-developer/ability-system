using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TargetType
{
    public enum TargetTypeItem
    {
        NONE,
        ANY,
        ALLIES,
        ENEMIES,
        RECT_COLLIDER,
        SPHERE_COLLIDER,
        RAY
    }

    public interface ITargetType
    {
        TargetTypeItem Type { get; }

        List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer);

#if DEBUG
        void Draw(Transform owner);
#endif
    }
}
