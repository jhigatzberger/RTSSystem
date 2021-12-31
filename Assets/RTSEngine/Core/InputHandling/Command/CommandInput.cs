using RTSEngine.Core.Selection;
using RTSEngine.Core;
using System;
using UnityEngine;

namespace RTSEngine.Core.InputHandling
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
                    SelectionContext.locked = value != null;

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
                    print(value == null);
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
            if (ContextCommand == null || ForcedCommand != null)
                return;
            DistributeCommandToSelection(ContextCommand);
        }
        public void DistributeForcedCommand()
        {
            if (ForcedCommand == null)
                return;
            DistributeCommandToSelection(ForcedCommand);
            ClearForcedCommand();
        }
        public void ClearForcedCommand()
        {
            ForcedCommand = null;
        }

        public static void DistributeCommandToSelection(Command command)
        {
            int playerTeam = Team.TeamContext.PlayerTeam;
            foreach (ISelectable selectable in SelectionContext.selection)
            {
                if(selectable.Behaviour.TryGetExtension(out ICommandable commandable))
                {
                    if (commandable.Behaviour.Team == playerTeam && command.Applicable(commandable))
                    {
                        CommandContext.EnqueueCommand(
                            new DistributedCommand
                            {
                                data = command.Build(commandable),
                                clearQueueOnEnqeue = shouldClearQueueOnInput,
                                entity = commandable.Behaviour.Id
                            }
                        );
                    }                        
                }                    
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
            if (SelectionContext.selection.Count == 0)
                CachedEntity = null;
            else if (CachedEntity == null || CachedEntity.Behaviour != SelectionContext.selection[0].Behaviour)
            {
                print(SelectionContext.selection[0].Behaviour.Team +" "+ Team.TeamContext.PlayerTeam);
                if (SelectionContext.selection[0].Behaviour.TryGetExtension(out ICommandable commandable) && SelectionContext.selection[0].Behaviour.Team == Team.TeamContext.PlayerTeam)
                {
                    CachedEntity = commandable;
                }
                else
                    CachedEntity = null;
            }
        }
    }

}
