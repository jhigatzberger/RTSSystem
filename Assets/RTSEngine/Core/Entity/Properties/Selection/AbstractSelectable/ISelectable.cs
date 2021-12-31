using System;

namespace RTSEngine.Core.Selection
{
    public interface ISelectable : IExtension
    {
        public int Priority { get; }
        public bool Selected { get; }
        public int SelectionIndex { get; }
        public void Select(int index);
        public void Deselect();
        public bool Visible { get; }
        public event Action<bool> OnSelectedUpdate;
    }

}
