using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    public static class SelectionContext
    {
        public const int NULL_PRIORITY = -1000;

        public static bool locked = false;

        public static event Action OnSelectionUpdate;

        public static List<ISelectable> selection = new List<ISelectable>();
        public static HashSet<ISelectable> onScreen = new HashSet<ISelectable>();

        private static int _priority = NULL_PRIORITY;
        public static int Priority {
            get => _priority;
            set
            {
                if(value != _priority)
                {
                    if (value > _priority)
                        Deselect();
                    _priority = value;
                }
            }
        }
        #region Hovering
        public static HashSet<IExtendableEntity> hovered = new HashSet<IExtendableEntity>();
        public static IExtendableEntity FirstOrNullHovered
        {
            get
            {
                if (hovered.Count < 1)
                    return null;
                else
                    return hovered.First();
            }
        }
        #endregion

        private static int FindPriority()
        {
            int priority = NULL_PRIORITY;
            foreach (ISelectable selectable in selection)
                if (selectable.Priority > priority)
                    priority = selectable.Priority;
            return priority;
        }
        private static void UpdateSelection()
        {
            for (int i = 0; i<selection.Count; i++)
                selection[i].Select(i);
            OnSelectionUpdate?.Invoke();
        }
        private static void HandleDeselect(ISelectable selectable)
        {
            selectable.Deselect();
        }
        private static void HandleSelect(ISelectable selectable)
        {
            if (selectable.Priority > Priority)
                Priority = selectable.Priority;
            selection.Add(selectable);
            selectable.Select(selection.Count - 1);
        }

        public static void Select(ISelectable selectable)
        {
            if (locked)
                return;
            if (selectable == null || selectable.Priority < Priority || selectable.Selected || !selectable.Entity.MonoBehaviour.enabled)
                return;
            HandleSelect(selectable);
            OnSelectionUpdate?.Invoke();

        }
        public static void Select(IEnumerable<ISelectable> items)
        {
            if (locked)
                return;
            foreach (ISelectable selectable in items)
            {
                if (selectable == null || selectable.Priority < Priority || selectable.Selected || !selectable.Entity.MonoBehaviour.enabled)
                    continue;
                HandleSelect(selectable);
            }
            OnSelectionUpdate?.Invoke();
        }
        public static void Deselect(Func<ISelectable, bool> p, bool updatePriority = true)
        {
            if (locked)
                return;
            foreach (ISelectable selectable in selection.Where(p))
                HandleDeselect(selectable);
            selection.RemoveAll(new Predicate<ISelectable>(p));
            if (updatePriority)
                Priority = FindPriority();
            UpdateSelection();
        }
        public static void Deselect(ISelectable selectable, bool updatePriority = true)
        {
            if (locked || selectable == null || !selectable.Selected)
                return;
            HandleDeselect(selectable);
            selection.Remove(selectable);
            if (selectable.Priority == Priority && updatePriority)
                Priority = FindPriority();
            UpdateSelection();
        }
        public static void Deselect()
        {
            if (locked)
                return;
            Priority = NULL_PRIORITY;
            foreach (ISelectable selectable in selection)
                HandleDeselect(selectable);
            selection.Clear();
        }
    }
}