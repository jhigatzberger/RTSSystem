using JHiga.RTSEngine.CommandPattern;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.UI
{
    public class CommandCursorData : MonoBehaviour
    {
        [SerializeField] private CommandCursor[] commandCursors;
        [SerializeField] public BaseCursor defaultCursor;

        private Dictionary<CommandProperties, CommandCursor> commandToCursor;

        private void Start()
        {
            commandToCursor = new Dictionary<CommandProperties, CommandCursor>();
            foreach (CommandCursor cursor in commandCursors)
                commandToCursor.Add(cursor.command, cursor);
            CommandInput.Instance.OnContextCommand += UpdateCursor;
            CommandInput.Instance.OnForcedCommand += UpdateCursor;
            defaultCursor.Set();
        }

        private void OnDestroy()
        {
            CommandInput.Instance.OnContextCommand -= UpdateCursor;
            CommandInput.Instance.OnForcedCommand -= UpdateCursor;
        }

        private void UpdateCursor(CommandProperties obj)
        {
            if (obj == null || !commandToCursor.TryGetValue(obj, out CommandCursor cursor))
                defaultCursor.Set();
            else
                cursor.Set();
        }
    }
}
