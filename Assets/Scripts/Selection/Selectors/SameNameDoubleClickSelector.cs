
using UnityEngine;
using System.Linq;

namespace RTS
{
    public class SameNameDoubleClickSelector : Selector
    {
        [SerializeField] private float maxTimeBetweenClicks = 0.3f;
        private float lastUpTime;
        protected override bool Applicable {
            get
            {
                return Time.time-lastUpTime < maxTimeBetweenClicks && Manager.Selection.items.Count == 1;
            }
        }

        public override int Prority
        {
            get
            {
                return 1;
            }
        }

        private void Start()
        {
            lastUpTime = Time.time - maxTimeBetweenClicks;
        }


        public override void InputStop()
        {
            if (Applicable)
            {
                string name = Manager.Selection.First.name;
                Manager.Selection.AddRange(SelectableOnScreenObject.current.Where(s => s.name == name));
            }
            lastUpTime = Time.time;
        }

        public override void InputStart()
        {
        }
    }
}