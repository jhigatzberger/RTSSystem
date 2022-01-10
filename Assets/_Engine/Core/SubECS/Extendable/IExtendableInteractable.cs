using UnityEngine;

namespace JHiga.RTSEngine
{
    public interface IExtendableInteractable
    {
        public int EntityId { get; }
        public int PlayerId { get; }
        public MonoBehaviour MonoBehaviour { get; }
        public IInteractableExtension[] Extensions { get; set; }
        public T GetScriptableComponent<T>() where T : IInteractableExtension;
        public bool TryGetScriptableComponent<T>(out T extension) where T : IInteractableExtension;
        public void Clear();
    }
}
