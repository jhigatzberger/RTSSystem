using RTSEngine.Core;
using RTSEngine.Core.InputHandling;
using UnityEngine;
using UnityEngine.UI;

public class SelectionContextCommandButton : MonoBehaviour
{
    private Command command;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void Clear()
    {
        command = null;
        image.sprite = null;
        image.enabled = false;
    }
    public void Set(Command command)
    {
        this.command = command;
        image.sprite = command.icon;
        image.enabled = true;
    }
    public void BuildCommand()
    {
        if(command.Applicable(CommandInput.CachedEntity))
        {
            if (!command.requireContext)
                CommandInput.DistributeCommandToSelection(command);
            else
                CommandInput.ForcedCommand = command;
        }
    }
}
