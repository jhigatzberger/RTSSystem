using UnityEngine;

namespace JHiga.RTSEngine
{
    public interface IExtendable
    {
        public int EntityId { get; }
        public int PlayerId { get; }
        public MonoBehaviour MonoBehaviour { get; }
        public IExtension[] ScriptableComponents { get; set; }
        public T GetScriptableComponent<T>() where T : IExtension;
        public bool TryGetScriptableComponent<T>(out T extension) where T : IExtension;
        public void Clear();
    }
}
