using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class CursorManager : MonoBehaviour
    {
        public Texture2D[] cursors;
        public Texture2D defaultCursor;

        private void Awake()
        {
            Entity.CommandInput.OnContextCommand += CommandInput_OnContextCommand;
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