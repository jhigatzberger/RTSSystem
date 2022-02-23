using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Collider))]
    public class SelectionBehaviour : MonoBehaviour, ISelectionBehaviour
    {
        private Renderer _renderer;
        private ISelectable _selectable;
        public ISelectable Selectable
        {
            get => _selectable;
            set
            {
                if (_selectable == null)
                {
                    _selectable = value;
                    _renderer = GetComponent<Renderer>();

                    Selectable.OnSelectedUpdate += UpdateVisuals;
                    UpdateVisuals(Selectable.Selected);
                }
            }
        }
        private void OnBecameVisible()
        {
            SelectionContext.onScreen.Add(Selectable);
        }
        private void OnBecameInvisible()
        {
            SelectionContext.onScreen.Remove(Selectable);
        }
        private Color GetColor(bool selected)
        {
            if (!selected)
                return new Color(0, 0, 0, 0);
            if (Selectable.Entity.UID.player == PlayerContext.PlayerId)
                return new Color(0, 1, 0, 0.7f);
            else
                return new Color(1, 1, 1, 0.7f);
        }
        public void UpdateVisuals(bool selected)
        {
            _renderer.material.color = GetColor(selected);
        }

        void OnDisable()
        {
            GetComponent<Collider>().enabled = false;
        }
        void OnEnable()
        {
            GetComponent<Collider>().enabled = true;
        }
    }
}