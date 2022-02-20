using UnityEngine;

namespace JHiga.RTSEngine
{
    public interface IExtendableEntity
    {
        public UID UniqueID { get; }
        public Vector3 Position { get; }
        public Vector3 ClosestEdgePoint(Vector3 pos);
        public MonoBehaviour MonoBehaviour { get; }
        public IEntityExtension[] Extensions { get; set; }
        public T GetExtension<T>() where T : IEntityExtension;
        public bool TryGetExtension<T>(out T extension) where T : IEntityExtension;
        public void Clear();
        public void Disable(bool setGameObjectInactive = false);
    }
}
