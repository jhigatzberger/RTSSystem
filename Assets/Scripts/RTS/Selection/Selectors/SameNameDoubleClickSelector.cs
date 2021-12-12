
using UnityEngine;
using System.Linq;

namespace RTS.Selection.Selectors
{
    public class SameNameDoubleClickSelector : Selector
    {
        [SerializeField] private float maxTimeBetweenClicks = 0.3f;
        private float lastUpTime;
        protected override bool Applicable {
            get
            {
                return Time.time-lastUpTime < maxTimeBetweenClicks && Context.items.Count == 1;
            }
        }

        public override int Priority => 1;

        private void Start()
        {
            lastUpTime = Time.time - maxTimeBetweenClicks;
        }


        public override void InputStop()
        {
            if (Applicable)
            {
                string name = Context.First.name;
                Context.Select(Context.onScreen.Where(s => s.controller.name == name).Select(s => s.controller)); // BROKEN?
            }
            lastUpTime = Time.time;
        }

        public override void InputStart()
        {
        }
    }
}