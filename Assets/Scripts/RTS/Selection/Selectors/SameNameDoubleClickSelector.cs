
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
                return Time.time-lastUpTime < maxTimeBetweenClicks && Context.Selection.items.Count == 1;
            }
        }

        public override int Prority => 1;

        private void Start()
        {
            lastUpTime = Time.time - maxTimeBetweenClicks;
        }


        public override void InputStop()
        {
            if (Applicable)
            {
                string name = Context.Selection.First.name;
                Context.Selection.AddRange(Context.onScreen.Where(s => s.controller.name == name).Select(s => s.controller)); // BROKEN?
            }
            lastUpTime = Time.time;
        }

        public override void InputStart()
        {
        }
    }
}