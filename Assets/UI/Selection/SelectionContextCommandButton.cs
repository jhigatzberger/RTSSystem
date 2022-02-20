using JHiga.RTSEngine.CommandPattern;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JHiga.RTSEngine.UI
{
    public class SelectionContextCommandButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private CommandProperties command;
        private Image image;
        private GameObject descriptionPanel;

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
        public void Set(CommandProperties command, GameObject descriptionPanel)
        {
            this.descriptionPanel = descriptionPanel;
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
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("MOUSEOVER");
            if (command != null && !command.Description.Equals(""))
            {
                descriptionPanel.SetActive(true);
                descriptionPanel.GetComponentInChildren<Text>().text = command.Description;
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            descriptionPanel.SetActive(false);
        }
    }
}
