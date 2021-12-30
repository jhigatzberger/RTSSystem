using RTSEngine.Entity.Selection;
using System;
using UnityEngine;

namespace RTSEngine.Entity
{
    public class CommandInput : MonoBehaviour
    {
        public static Command _contextCommand;
        public static Command ContextCommand
        {
            get => _contextCommand;
            set
            {
                if(value != _contextCommand)
                {
                    _contextCommand = value;
                    OnContextCommand?.Invoke(value == null ? -1 : value.id);
                }
            }
        }

        private static Command _forcedCommand;
        public static Command ForcedCommand
        {
            get => _forcedCommand;
            set
            {
                if(value != _forcedCommand)
                {
                    Context.locked = value != null;

                    _forcedCommand = value;
                    ContextCommand = value;
                }
            }
        }

        public static event Action<int> OnContextCommand;
        public static event Action<ICommandable> OnCommandableSelectionEntity;
        private static ICommandable _cachedEntity;
        public static ICommandable CachedEntity
        {
            get { return _cachedEntity; }
            set
            {
                if (value != _cachedEntity)
                {
                    _cachedEntity = value;
                    OnCommandableSelectionEntity?.Invoke(value);
                }
            }
        }

        public static bool shouldClearQueueOnInput = true;
        public void SetClearQueueOnInput(bool shouldClearQueueOnInput)
        {
            CommandInput.shouldClearQueueOnInput = shouldClearQueueOnInput;
        }
        public void DistributeContextCommand()
        {
            print("DistributeContextCommand " + ContextCommand);
            if (ContextCommand == null || ForcedCommand != null)
                return;
            DistributeCommand(ContextCommand);
        }
        public void DistributeForcedCommand()
        {
            if (ForcedCommand == null)
                return;
            DistributeCommand(ForcedCommand);
            ClearForcedCommand();
        }
        public void ClearForcedCommand()
        {
            ForcedCommand = null;
        }

        public static void DistributeCommand(Command command)
        {
            print("DistributeCommand " + command);
            int playerTeam = Team.Context.PlayerTeam;
            foreach (ICommandable commandable in Context.entities)
            {
                if (commandable.Entity.Team == playerTeam && command.Applicable(commandable))
                    CommandContext.EnqueueCommand(
                        new DistributedCommand
                        {
                            data = command.Build(commandable),
                            clearQueueOnEnqeue = shouldClearQueueOnInput,
                            entity = commandable.Entity.id
                        }
                    );
            }
        }

        private void Update()
        {
            CacheEntity();
            if (ForcedCommand != null)
                return;
            if (CachedEntity != null)
                ContextCommand = CachedEntity.FirstApplicableDynamicallyBuildableCommand;
            else
                ContextCommand = null;
        }

        private void CacheEntity()
        {
            if (Context.entities.Count == 0)
                CachedEntity = null;
            else if (CachedEntity == null || CachedEntity.Entity != Context.entities[0])
            {
                if (Context.entities[0] is ICommandable && Context.entities[0].Team == Team.Context.PlayerTeam)
                    CachedEntity = (ICommandable)Context.entities[0];
                else
                    CachedEntity = null;
            }
        }
    }
    public struct DistributedCommand : IEquatable<DistributedCommand>
    {
        public CommandData data;
        public int entity;
        public bool clearQueueOnEnqeue;

        public bool Equals(DistributedCommand other)
        {
            return other.data.Equals(data) && entity == other.entity;
        }
    }
}
