using System.Collections.Generic;
using System;
using System.Linq;

namespace RTSEngine.Entity.Selection
{
    public static class Context
    {
        public const int NULL_PRIORITY = -1000;

        public static bool locked = false;


        public static event Action OnSelectionUpdate;
        public static List<BaseEntity> entities = new List<BaseEntity>();
        public static HashSet<SelectableEntity> onScreen = new HashSet<SelectableEntity>();
        private static int _priority = NULL_PRIORITY;
        public static int Priority {
            get
            {
                return _priority;
            }
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

        private static int FindPriority()
        {
            int priority = NULL_PRIORITY;
            foreach (BaseEntity entity in entities)
                if (entity.Priority > priority)
                    priority = entity.Priority;
            return priority;
        }
        private static void UpdateSelection()
        {
            for (int i = 0; i<entities.Count; i++)
                entities[i].SelectionPosition = i;
            OnSelectionUpdate?.Invoke();
        }
        private static void HandleDeselect(BaseEntity entity)
        {
            entity.SelectionPosition = -1;
        }
        private static void HandleSelect(BaseEntity entity)
        {
            if (entity.Priority > Priority)
                Priority = entity.Priority;
            entities.Add(entity);
            entity.SelectionPosition = entities.Count - 1;
        }

        public static void Select(BaseEntity entity)
        {
            if (locked)
                return;
            if (entity == null || entity.Priority < Priority || entity.Selected || !entity.enabled)
                return;
            HandleSelect(entity);
            OnSelectionUpdate?.Invoke();

        }
        public static void Select(IEnumerable<BaseEntity> items)
        {
            if (locked)
                return;
            foreach (BaseEntity entity in items)
            {
                if (entity == null || entity.Priority < Priority || entity.Selected || !entity.enabled)
                    continue;
                HandleSelect(entity);
            }
            OnSelectionUpdate?.Invoke();
        }
        public static void Deselect(Func<BaseEntity, bool> p, bool updatePriority = true)
        {
            if (locked)
                return;
            foreach (BaseEntity entity in entities.Where(p))
                HandleDeselect(entity);
            entities.RemoveAll(new Predicate<BaseEntity>(p));
            if (updatePriority)
                Priority = FindPriority();
            UpdateSelection();
        }
        public static void Deselect(BaseEntity entity, bool updatePriority = true)
        {
            if (locked || entity == null || !entity.Selected)
                return;

            HandleDeselect(entity);
            entities.Remove(entity);
            if (entity.Priority == Priority && updatePriority)
                Priority = FindPriority();
            UpdateSelection();
        }
        public static void Deselect()
        {
            if (locked)
                return;
            Priority = NULL_PRIORITY;
            foreach (BaseEntity entity in entities)
                HandleDeselect(entity);
            entities.Clear();
        }

    }
}