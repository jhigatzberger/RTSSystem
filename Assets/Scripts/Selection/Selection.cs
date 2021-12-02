using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RTS
{
    public class Selection
    {
        public HashSet<SelectableObject> items;
        public int priority;
        public Selection(IEnumerable<SelectableObject> items = null, int priority = -1)
        {
            if (items == null)
                items = new HashSet<SelectableObject>();
            this.items = new HashSet<SelectableObject>(items);
            if (priority < 0)
                priority = FindPriority();
            this.priority = priority;
        }

        private int FindPriority()
        {
            int priority = 0;
            foreach (SelectableObject selectableObject in items)
                if (selectableObject.ownPriority > priority)
                    priority = selectableObject.ownPriority;
            return priority;
        }

        public void Add(SelectableObject selectableObject)
        {
            if (selectableObject == null || selectableObject.UnselectableForceSelection != null)
                return;

            if (items.Add(selectableObject))
            {
                if (selectableObject.ownPriority > priority)
                    priority = selectableObject.ownPriority;
                selectableObject.JoinedSelection = this;
            }
        }
        public void AddRange(IEnumerable<SelectableObject> items)
        {
            foreach (SelectableObject selectableObject in items)
                Add(selectableObject);
        }

        internal void RemoveWhere(Func<SelectableObject, bool> p, bool updatePriority = true)
        {
            foreach (SelectableObject selectableObject in items.Where(p))
                selectableObject.JoinedSelection = null;
            items.RemoveWhere(new Predicate<SelectableObject>(p));
            if (updatePriority)
                priority = FindPriority();
        }

        public void Remove(SelectableObject selectableObject, bool updatePriority = true)
        {
            if (selectableObject == null)
                return;

            if (items.Remove(selectableObject))
            {
                if (selectableObject.ownPriority == priority && updatePriority)
                    priority = FindPriority();
                selectableObject.JoinedSelection = null;
            }
        }

        public void Clear()
        {
            priority = 0;
            foreach (SelectableObject selectableObject in items)
                selectableObject.JoinedSelection = null;
            items.Clear();
        }

        public SelectableObject First {
            get
            {
                return items.First();
            }
        }

    }
}