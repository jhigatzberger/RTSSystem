using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RTS
{
    public class Selection
    {
        public HashSet<BaseEntity> items;
        public int priority;
        public Selection(IEnumerable<BaseEntity> items = null, int priority = -1)
        {
            if (items == null)
                items = new HashSet<BaseEntity>();
            this.items = new HashSet<BaseEntity>(items);
            if (priority < 0)
                priority = FindPriority();
            this.priority = priority;
        }

        private int FindPriority()
        {
            int priority = 0;
            foreach (BaseEntity selectableObject in items)
                if (selectableObject.Priority > priority)
                    priority = selectableObject.Priority;
            return priority;
        }

        public void Add(BaseEntity selectableObject)
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
        public void AddRange(IEnumerable<BaseEntity> items)
        {
            foreach (BaseEntity selectableObject in items)
                Add(selectableObject);
        }

        internal void RemoveWhere(Func<BaseEntity, bool> p, bool updatePriority = true)
        {
            foreach (BaseEntity selectableObject in items.Where(p))
                selectableObject.Selection = null;
            items.RemoveWhere(new Predicate<BaseEntity>(p));
            if (updatePriority)
                priority = FindPriority();
        }

        public void Remove(BaseEntity selectableObject, bool updatePriority = true)
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
            foreach (BaseEntity selectableObject in items)
                selectableObject.Selection = null;
            items.Clear();
        }

        public BaseEntity First {
            get
            {
                return items.First();
            }
        }

    }
}