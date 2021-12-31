using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTSEngine.Core.InputHandling
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

            CommandContext.OnCommandDistribute += DistributeCommand;
        }

        public void DistributeCommand(DistributedCommand command)
        {
            RTSBehaviour entity = EntityContext.entities[command.entity];
            if (entity != null && entity.TryGetComponent(out ICommandable commandable))
            {
                if (command.clearQueueOnEnqeue)
                    commandable.ClearCommands();
                commandable.Enqueue(command.data);
            }
        }
        public static void Execute(ICommandable commandable, CommandData commandData)
        {
            Command command = Commands[commandData.commandID];
            command.Execute(commandable, commandData);
        }
        
    }

}
