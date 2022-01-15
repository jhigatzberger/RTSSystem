using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    public struct Target
    {
        public Vector3 position;
        public IExtendableEntity entity;
        public Target(SkinnedTarget skinnedTarget)
        {
            if (skinnedTarget.targetEntity >= 0)
                entity = GameEntity.Get(new UID(skinnedTarget.targetEntity));
            else
                entity = null;
            position = skinnedTarget.position;
        }
        public Vector3 Point => entity == null ? position : entity.MonoBehaviour.transform.position;
        public float Distance(Vector3 point)
        {
            return Vector3.Distance(Point, point);
        }
        public SkinnedTarget Skin => new SkinnedTarget
        {
            position = position,
            targetEntity = entity==null?-1:entity.UniqueID.uniqueId
        };
    }
    public struct SkinnedTarget
    {
        public Vector3 position;
        public int targetEntity;
    }
}