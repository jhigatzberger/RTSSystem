using RTSEngine.Entity;
using RTSEngine.Entity.Selection;
using UnityEngine;

public class SelectionContextCommandPanel : MonoBehaviour
{
    Command[] _commands;

    SelectionContextCommandButton[] commandButtons;

    public Command[] Commands
    {
        get => _commands;
        set
        {
            if (value != _commands)
            {
                _commands = value;
                foreach (SelectionContextCommandButton button in commandButtons)
                    button.Clear();
            }
            if (value != null)
            {
                int length = value.Length < commandButtons.Length ? value.Length : commandButtons.Length;
                for (int i = 0; i < length; i++)
                {
                    commandButtons[i].Set(value[i]);
                }
            }
        }
    }
    void Start()
    {
        commandButtons = GetComponentsInChildren<SelectionContextCommandButton>();
        CommandInput.OnCommandableSelectionEntity += CommandInput_OnCommandableSelectionEntity;
    }

    private void CommandInput_OnCommandableSelectionEntity(ICommandable obj)
    {
        if (obj != null)
            Commands = obj.CommandCompetence;
        else
            Commands = null;
    }
}
