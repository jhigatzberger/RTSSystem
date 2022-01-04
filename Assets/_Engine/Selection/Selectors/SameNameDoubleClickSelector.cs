using UnityEngine;
using System.Linq;
using JHiga.RTSEngine.Selection;


public class SameNameDoubleClickSelector : Selector
{
    [SerializeField] private float maxTimeBetweenClicks = 0.3f;
    private float lastUpTime;
    protected override bool Applicable {
        get
        {
            return Time.time - lastUpTime < maxTimeBetweenClicks && SelectionContext.selection.Count == 1;
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
            string name = SelectionContext.selection[0].Extendable.MonoBehaviour.name;
            SelectionContext.Select(SelectionContext.onScreen.Where(s => s.Extendable.MonoBehaviour.name == name));
        }
        lastUpTime = Time.time;
    }

    public override void InputStart()
    {
    }
    
}