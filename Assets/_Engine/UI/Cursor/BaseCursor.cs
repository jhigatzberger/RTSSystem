using UnityEngine;

namespace JHiga.RTSEngine.UI
{
    [CreateAssetMenu(fileName = "BaseCursor", menuName = "RTS/Cursors/BaseCursor")]
    public class BaseCursor : ScriptableObject
    {
        public Texture2D texture;
        public Vector2 offset;
        public CursorMode mode;
        public void Set()
        {
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
    }
}