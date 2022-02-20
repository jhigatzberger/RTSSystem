using JHiga.RTSEngine.InputHandling;
using JHiga.RTSEngine.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    public struct Target : IEquatable<Target>
    {
        public Vector3 position;
        public IExtendableEntity entity;
        public bool HasActiveEntity => entity != null && entity.MonoBehaviour.enabled;
        public Target(SkinnedTarget skinnedTarget)
        {
            if (skinnedTarget.targetEntity >= 0)
                entity = GameEntity.Get(new UID(skinnedTarget.targetEntity));
            else
                entity = null;
            position = Vector2ToVector3(skinnedTarget.position);
        }
        public Vector3 ClosestPoint(Vector3 position)
        {
            if (entity != null)
                return entity.ClosestEdgePoint(position);
            return this.position;
        }
        public float Distance(Vector3 point)
        {
            return Vector3.Distance(ClosestPoint(point), point);
        }
        public SkinnedTarget Skin => new SkinnedTarget
        {
            position = new Vector2(position.x, position.z),
            targetEntity = entity == null ? -1 : entity.UniqueID.uniqueId
        };
        public static Vector3 Vector2ToVector3(Vector2 vector2)
        {
            if (Physics.Raycast(new Vector3(vector2.x, RTSWorldData.Instance.maxMapHight, vector2.y), Vector3.down, out RaycastHit hit, RTSWorldData.Instance.groundLayerMask))
                return hit.point;
            throw new Exception("Target position out of map!");
        }
        public bool IsInRange(Vector3 position, float range)
        {
            if (entity != null)
                return Vector3.Distance(position, entity.MonoBehaviour.transform.position) < range;
            return Vector3.Distance(position, this.position) < range;
        }
        public bool Equals(Target other)
        {
            return position == other.position && entity == other.entity;
        }
        public static Target FromContext => new Target
        {
            position = InputManager.worldPointerPosition.Value,
            entity = SelectionContext.FirstOrNullHovered
        };
    }
    public struct SkinnedTarget
    {
        public Vector2 position;
        public int targetEntity;
    }
}