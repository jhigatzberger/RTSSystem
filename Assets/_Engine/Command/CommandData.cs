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
        public Dictionary<int, CommandProperties> IdToCommand
        {
            get
            {
                if(Instance.commandMap == null)
                {
                    Instance.commandMap = new Dictionary<int, CommandProperties>();
                    foreach (CommandProperties command in Instance._commands)
                        Instance.commandMap.Add(command.id, command);
                }
                return Instance.commandMap;
            }
        }
        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}
