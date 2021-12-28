using System.Collections.Generic;
using System;
using System.Linq;

namespace RTSEngine.Entity.Selection
{
    public static class Context
    {
        public static List<BaseEntity> entities = new List<BaseEntity>();
        public static HashSet<SelectableEntity> onScreen = new HashSet<SelectableEntity>();
        private static int _priority = 0;
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
            int priority = 0;
            foreach (BaseEntity entity in entities)
                if (entity.Priority > priority)
                    priority = entity.Priority;
            return priority;
        }
        private static void UpdateSelection()
        {
            for (int i = 0; i<entities.Count; i++)
                entities[i].SelectionPosition = i;
        }
        private static void HandleDeselect(BaseEntity entity)
        {
            entity.SelectionPosition = -1;
        }
        private static void HandleSelect(BaseEntity entity)
        {
            entity.SelectionPosition = entities.Count - 1;
        }

        public static void Select(BaseEntity entity)
        {
            if (entity == null || entity.Priority < Priority || entity.Selected || !entity.enabled)
                return;

            if (entity.Priority > Priority)
                Priority = entity.Priority;
            entities.Add(entity);
            HandleSelect(entity);
        }
        public static void Select(IEnumerable<BaseEntity> items)
        {
            foreach (BaseEntity entity in items)
                Select(entity);
        }
        public static void Deselect(Func<BaseEntity, bool> p, bool updatePriority = true)
        {
            foreach (BaseEntity entity in entities.Where(p))
                HandleDeselect(entity);
            entities.RemoveAll(new Predicate<BaseEntity>(p));
            if (updatePriority)
                Priority = FindPriority();
            UpdateSelection();
        }
        public static void Deselect(BaseEntity entity, bool updatePriority = true)
        {
            if (entity == null || !entity.Selected)
                return;

            HandleDeselect(entity);
            entities.Remove(entity);
            if (entity.Priority == Priority && updatePriority)
                Priority = FindPriority();
            UpdateSelection();
        }
        public static void Deselect()
        {
            Priority = 0;
            foreach (BaseEntity entity in entities)
                HandleDeselect(entity);
            entities.Clear();
        }

    }
}