using UnityEngine;
using UnityEngine.UI;
using JHiga.RTSEngine.InputHandling;
using JHiga.RTSEngine.CommandPattern;

public class CommandDisplay : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
        CommandEvents.OnCommandDistributionRequested += DisplayCommandEN;
        CommandEvents.OnCommandEnqueueRequested += DisplayCommandEX;
    }

    public void DisplayCommandEX(DistributableCommand c)
    {
        text.text = "Executing command";
    }
    public void DisplayCommandEN(DistributableCommand c)
    {
        text.text = "Command in queue";
    }
}
