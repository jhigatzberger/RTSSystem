using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandData : ScriptableObject
    {
        private static CommandData _instance;
        public static CommandData Instance
        {
            get
            {
                if (_instance == null)
                {
                    CommandData[] assets = Resources.LoadAll<CommandData>("");
                    if (assets.Length > 0)
                        _instance = assets[0];
                }
                return _instance;
            }
        }
        public List<CommandProperties> commands = new List<CommandProperties>();
        private Dictionary<int, CommandProperties> commandMap;
        private Dictionary<CommandProperties, int> reverseCommandMap;
        public Dictionary<int, CommandProperties> IdToCommand
        {
            get
            {
                if (commandMap == null)
                    InitMaps();
                return commandMap;
            }
        }
        public Dictionary<CommandProperties, int> CommandToId
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
            commandMap = new Dictionary<int, CommandProperties>();
            for (int i = 0; i < commands.Count; i++)
                commandMap.Add(i, commands[i]);
            reverseCommandMap = new Dictionary<CommandProperties, int>();
            for (int i = 0; i < commands.Count; i++)
                reverseCommandMap.Add(commands[i], i);
        }
    }
}
