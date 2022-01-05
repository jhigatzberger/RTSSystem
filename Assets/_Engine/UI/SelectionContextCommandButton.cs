using JHiga.RTSEngine.CommandPattern;
using UnityEngine;
using UnityEngine.UI;

namespace JHiga.RTSEngine.UI
{
    public class SelectionContextCommandButton : MonoBehaviour
    {
        private CommandProperties command;
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
        public void Set(CommandProperties command)
        {
            this.command = command;
            image.sprite = command.icon;
            image.enabled = true;
        }
        public void BuildCommand()
        {
            CommandInput.Instance.ForcedCommand = command;
            if (!command.requireContext)
                CommandInput.Instance.RequestForcedCommand();
        }
    }
}
