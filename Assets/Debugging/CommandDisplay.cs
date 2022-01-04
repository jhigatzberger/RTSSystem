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
        CommandContext.OnCommandEnqueue += DisplayCommandEN;
        CommandContext.OnCommandDistribute += DisplayCommandEX;
    }

    public void DisplayCommandEX(DistributedCommand c)
    {
        text.text = "Executing command";
    }
    public void DisplayCommandEN(DistributedCommand c)
    {
        text.text = "Command in queue";
    }
}
