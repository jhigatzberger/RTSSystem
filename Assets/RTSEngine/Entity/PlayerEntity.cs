using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RTSEngine.Entity;
using RTSEngine.Entity.AI;

public class PlayerEntity : BaseEntity, ICommandable
{
    public BaseEntity Entity { get; set; }

    [SerializeField] private int _priority;
    public override int Priority => Team == RTSEngine.Team.Context.PlayerTeam ? _priority : _priority-RTSEngine.Entity.Selection.Context.NULL_PRIORITY;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Current.HasValue)
            Gizmos.DrawSphere(Current.Value.position, 1);
    }
    #region Command
    [Tooltip("Watch out for the order! The earlier the command, the more it will be prioritized.")]
    [SerializeField] private Command[] commandCompetence;
    public Command[] CommandCompetence => commandCompetence;
    Queue<CommandData> commandQueue = new Queue<CommandData>();
    public CommandData? Current { get; set; }
    public event System.Action OnCommandClear;

    public void Enqueue(CommandData command)
    {
        if (!commandCompetence.Contains(CommandManager.Commands[command.commandID]))
            return;
        commandQueue.Enqueue(command);
        if (Current == null)
            ExecuteFirstCommand();
    }
    public void ExecuteFirstCommand()
    {
        if (commandQueue.Count == 0)
            return;
        Current = commandQueue.Dequeue();
        CommandManager.Execute(this, Current.Value);
    }
    public void ClearCommands()
    {
        OnCommandClear?.Invoke();
        Current = null;
        commandQueue.Clear();
    }
    public void Finish()
    {
        Current = null;
        ExecuteFirstCommand();
    }

    public void OnExitScene()
    {
        ClearCommands();
        GetComponent<Collider>().enabled = false;
    }

    public Command FirstApplicableDynamicallyBuildableCommand
    {
        get
        {
            foreach (Command command in commandCompetence)
                if (command.dynamicallyBuildable && command.Applicable(this))
                    return command;
            return null;
        }
    }

    #endregion


}


