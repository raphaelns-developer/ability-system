using System.Collections.Generic;
using UnityEngine;

namespace HOTG.Abilities.Target
{
    public enum TargetType
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
        TargetType Type { get; }

        List<Collider> GetTargets(Transform owner, LayerMask overrideTargetLayer);

#if DEBUG
        void Draw(Transform owner);
#endif
    }
}
