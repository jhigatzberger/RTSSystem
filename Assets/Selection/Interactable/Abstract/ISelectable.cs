using JHiga.RTSEngine;
using System;

namespace JHiga.RTSEngine.Selection
{
    public interface ISelectable : IEntityExtension
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
