using JHiga.RTSEngine.CommandPattern;
using UnityEngine;

namespace JHiga.RTSEngine.UI
{
    [CreateAssetMenu(fileName = "CommandCursor", menuName = "RTS/Cursors/CommandCursor")]
    public class CommandCursor : BaseCursor
    {
        public CommandProperties command;
    }
}