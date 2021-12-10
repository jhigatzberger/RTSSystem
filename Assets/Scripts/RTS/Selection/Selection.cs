using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RTS
{
    public class Selection
    {
        public HashSet<EntityController> items;
        public int priority;
        public Selection(IEnumerable<EntityController> items = null, int priority = -1)
        {
            if (items == null)
                items = new HashSet<EntityController>();
            this.items = new HashSet<EntityController>(items);
            if (priority < 0)
                priority = FindPriority();
            this.priority = priority;
        }

        private int FindPriority()
        {
            int priority = 0;
            foreach (EntityController selectableObject in items)
                if (selectableObject.Priority > priority)
                    priority = selectableObject.Priority;
            return priority;
        }

        public void Add(EntityController selectableObject)
        {
            if (selectableObject == null)
                return;

            if (items.Add(selectableObject))
            {
                if (selectableObject.Priority > priority)
                    priority = selectableObject.Priority;
                selectableObject.Selection = this;
            }
        }
        public void AddRange(IEnumerable<EntityController> items)
        {
            foreach (EntityController selectableObject in items)
                Add(selectableObject);
        }

        internal void RemoveWhere(Func<EntityController, bool> p, bool updatePriority = true)
        {
            foreach (EntityController selectableObject in items.Where(p))
                selectableObject.Selection = null;
            items.RemoveWhere(new Predicate<EntityController>(p));
            if (updatePriority)
                priority = FindPriority();
        }

        public void Remove(EntityController selectableObject, bool updatePriority = true)
        {
            if (selectableObject == null)
                return;

            if (items.Remove(selectableObject))
            {
                if (selectableObject.Priority == priority && updatePriority)
                    priority = FindPriority();
                selectableObject.Selection = null;
            }
        }

        public void Clear()
        {
            priority = 0;
            foreach (EntityController selectableObject in items)
                selectableObject.Selection = null;
            items.Clear();
        }

        public EntityController First {
            get
            {
                return items.First();
            }
        }

    }
}