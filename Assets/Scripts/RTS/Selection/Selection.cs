using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RTS
{
    public class Selection
    {
        public HashSet<ContextController> items;
        public int priority;
        public Selection(IEnumerable<ContextController> items = null, int priority = -1)
        {
            if (items == null)
                items = new HashSet<ContextController>();
            this.items = new HashSet<ContextController>(items);
            if (priority < 0)
                priority = FindPriority();
            this.priority = priority;
        }

        private int FindPriority()
        {
            int priority = 0;
            foreach (ContextController selectableObject in items)
                if (selectableObject.priority > priority)
                    priority = selectableObject.priority;
            return priority;
        }

        public void Add(ContextController selectableObject)
        {
            if (selectableObject == null)
                return;

            if (items.Add(selectableObject))
            {
                if (selectableObject.priority > priority)
                    priority = selectableObject.priority;
                selectableObject.JoinedSelection = this;
            }
        }
        public void AddRange(IEnumerable<ContextController> items)
        {
            foreach (ContextController selectableObject in items)
                Add(selectableObject);
        }

        internal void RemoveWhere(Func<ContextController, bool> p, bool updatePriority = true)
        {
            foreach (ContextController selectableObject in items.Where(p))
                selectableObject.JoinedSelection = null;
            items.RemoveWhere(new Predicate<ContextController>(p));
            if (updatePriority)
                priority = FindPriority();
        }

        public void Remove(ContextController selectableObject, bool updatePriority = true)
        {
            if (selectableObject == null)
                return;

            if (items.Remove(selectableObject))
            {
                if (selectableObject.priority == priority && updatePriority)
                    priority = FindPriority();
                selectableObject.JoinedSelection = null;
            }
        }

        public void Clear()
        {
            priority = 0;
            foreach (ContextController selectableObject in items)
                selectableObject.JoinedSelection = null;
            items.Clear();
        }

        public ContextController First {
            get
            {
                return items.First();
            }
        }

    }
}