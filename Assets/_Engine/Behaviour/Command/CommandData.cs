using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JHiga.RTSEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandData : MonoBehaviour
    {
        public static CommandData Instance { get; private set; }
        [SerializeField] private CommandProperties[] _commands;
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
            for (int i = 0; i < _commands.Length; i++)
                commandMap.Add(i, _commands[i]);
            reverseCommandMap = new Dictionary<CommandProperties, int>();
            for (int i = 0; i < _commands.Length; i++)
                reverseCommandMap.Add(_commands[i], i);
        }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}
