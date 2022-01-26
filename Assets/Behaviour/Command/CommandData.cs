using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandData : ScriptableObject
    {
        private static CommandData _instance;
        public static CommandData Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<CommandData>(Path.Generate<CommandData>());
                return _instance;
            }
        }
        public List<CommandProperties> commands = new List<CommandProperties>();
        private Dictionary<ushort, CommandProperties> commandMap;
        private Dictionary<CommandProperties, ushort> reverseCommandMap;
        public Dictionary<ushort, CommandProperties> IdToCommand
        {
            get
            {
                if (commandMap == null)
                    InitMaps();
                return commandMap;
            }
        }
        public Dictionary<CommandProperties, ushort> CommandToId
        {
            get
            {
                if (reverseCommandMap == null)
                    InitMaps();
                return reverseCommandMap;
            }
        }

        private void InitMaps()
        {
            commandMap = new Dictionary<ushort, CommandProperties>();
            for (ushort i = 0; i < commands.Count; i++)
                commandMap.Add(i, commands[i]);
            reverseCommandMap = new Dictionary<CommandProperties, ushort>();
            for (ushort i = 0; i < commands.Count; i++)
                reverseCommandMap.Add(commands[i], i);
        }
    }
}
