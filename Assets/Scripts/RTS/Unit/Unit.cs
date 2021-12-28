using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RTSEngine.Entity;
using RTSEngine.Entity.AI;

[RequireComponent(typeof(IMovable))]
[RequireComponent(typeof(IStateMachine))]
public class Unit : BaseEntity, ICommandable
{
    public BaseEntity Entity => this;
    public override int Priority => team == RTSEngine.Team.Context.playerTeam ? 10 : 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Current.HasValue)
            Gizmos.DrawSphere(Current.Value.position, 1);
    }
    #region Command
    [Tooltip("Watch out for the order! The earlier the command, the more it will be prioritized.")]
    public Command[] commandCompetence;
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

    public Command FirstApplicableCommand
    {
        get
        {
            foreach (Command command in commandCompetence)
                if (command.Applicable(this))
                    return command;
            return null;
        }
    }
    #endregion


}


