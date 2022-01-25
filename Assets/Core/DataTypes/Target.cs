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
            position = Vector2ToVector3(skinnedTarget.position);
        }
        public Vector3 Point => entity == null ? position : entity.MonoBehaviour.transform.position;
        public float Distance(Vector3 point)
        {
            return Vector3.Distance(Point, point);
        }
        public SkinnedTarget Skin => new SkinnedTarget
        {
            position = new Vector2(position.x, position.z),
            targetEntity = entity==null?-1:entity.UniqueID.uniqueId
        };
        public static Vector3 Vector2ToVector3(Vector2 vector2)
        {
            if(Physics.Raycast(new Vector3(vector2.x, RTSWorldData.Instance.maxMapHight, vector2.y), Vector3.down, out RaycastHit hit, RTSWorldData.Instance.groundLayerMask))
                return hit.point;
            throw new System.Exception("Target position out of map!");
        }
    }
    public struct SkinnedTarget
    {
        public Vector2 position;
        public int targetEntity;
    }
}