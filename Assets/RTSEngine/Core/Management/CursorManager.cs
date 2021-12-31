using UnityEngine;

namespace RTSEngine
{
    public class CursorManager : MonoBehaviour
    {
        public Texture2D[] cursors;
        public Texture2D defaultCursor;

        private void Awake()
        {
            Core.InputHandling.CommandInput.OnContextCommand += CommandInput_OnContextCommand;
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }

        private void CommandInput_OnContextCommand(int obj)
        {
            if (obj < 0 || obj >= cursors.Length)
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
            else
                Cursor.SetCursor(cursors[obj], Vector2.zero, CursorMode.Auto);
        }
    }

}
