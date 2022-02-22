using JHiga.RTSEngine.CommandPattern;
using System.Linq;
using UnityEngine;
namespace JHiga.RTSEngine.UI
{
    public class SelectionContextCommandPanel : MonoBehaviour
    {
        CommandProperties[] _commands;

        SelectionContextCommandButton[] commandButtons;

        public GameObject descriptionPanel;

        public CommandProperties[] Commands
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
                        commandButtons[i].Set(value[i], descriptionPanel);
                    }
                }
            }
        }
        void Start()
        {
            commandButtons = GetComponentsInChildren<SelectionContextCommandButton>();
            CommandInput.Instance.OnCachedEntityChanged += CommandInput_OnCommandableSelectionEntity;
            gameObject.SetActive(false);
        }

        private void CommandInput_OnCommandableSelectionEntity(ICommandable obj)
        {
            if (obj != null)
                Commands = obj.CommandCompetence.Where(c=>!c.dynamicallyBuildable).ToArray();
            else
                Commands = null;
            gameObject.SetActive(obj != null);
        }
    }
}