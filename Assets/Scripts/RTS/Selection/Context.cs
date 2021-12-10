using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RTS.Selection
{
    public static class Context
    {
        public static HashSet<BaseEntity> items = new HashSet<BaseEntity>();
        public static HashSet<SelectableObject> onScreen = new HashSet<SelectableObject>();
        public static int priority = 0;
        public static int Count => items.Count;
        public static BaseEntity First => items.First();
        private static int FindPriority()
        {
            int priority = 0;
            foreach (BaseEntity entity in items)
                if (entity.Priority > priority)
                    priority = entity.Priority;
            return priority;
        }
        public static void Select(BaseEntity entity)
        {
            if (entity == null)
                return;

            if (items.Add(entity))
            {
                if (entity.Priority > priority)
                    priority = entity.Priority;
                entity.Selected = true;
            }
        }
        public static void Select(IEnumerable<BaseEntity> items)
        {
            foreach (BaseEntity entity in items)
                Select(entity);
        }
        public static void Deselect(Func<BaseEntity, bool> p, bool updatePriority = true)
        {
            foreach (BaseEntity entity in items.Where(p))
                entity.Selected = false;
            items.RemoveWhere(new Predicate<BaseEntity>(p));
            if (updatePriority)
                priority = FindPriority();
        }
        public static void Deselect(BaseEntity entity, bool updatePriority = true)
        {
            if (entity == null)
                return;

            if (items.Remove(entity))
            {
                if (entity.Priority == priority && updatePriority)
                    priority = FindPriority();
                entity.Selected = false;
            }
        }
        public static void Deselect()
        {
            priority = 0;
            foreach (BaseEntity entity in items)
                entity.Selected = false;
            items.Clear();
        }
    }
}