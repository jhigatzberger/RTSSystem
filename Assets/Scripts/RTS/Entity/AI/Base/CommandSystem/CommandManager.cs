using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTS.Entity.AI
{
    public class CommandManager : MonoBehaviour
    {
        private static CommandManager instance;
        [SerializeField] private Command[] _commands;
        private Dictionary<int, Command> commandLookup;
        public static Dictionary<int, Command> Commands => instance.commandLookup;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            commandLookup = new Dictionary<int, Command>();
            foreach (Command command in _commands)
                commandLookup.Add(command.id, command);

            CommandContext.OnCommandExecute += ExecuteCommand;
        }

        public void ExecuteCommand(DistributedCommand command)
        {
            BaseEntity entity = EntityContext.entities[command.entity];
            if (entity != null && entity is AIEntity aiEntity)
                aiEntity.Enqueue(command.data);
        }
        
    }

}
