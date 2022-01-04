using JHiga.RTSEngine.Selection;
using JHiga.RTSEngine;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace JHiga.RTSEngine.CommandPattern
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
            int playerTeam = TeamContext.PlayerTeam;
            List<int> entites = new List<int>();
            foreach (ISelectable selectable in SelectionContext.selection)
            {
                if (selectable.Extendable.PlayerId == playerTeam)
                    entites.Add(selectable.Extendable.EntityId);                          
            }
            CommandContext.EnqueueCommand(
                new DistributedCommand
                {
                    data = command.Build(),
                    clearQueueOnEnqeue = shouldClearQueueOnInput,
                    entities = entites.ToArray()
                }
            );
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
            else if (CachedEntity == null || CachedEntity.Extendable != SelectionContext.selection[0].Extendable)
            {
                print(SelectionContext.selection[0].Extendable.PlayerId +" "+ TeamContext.PlayerTeam);
                if (SelectionContext.selection[0].Extendable.TryGetScriptableComponent(out ICommandable commandable) && SelectionContext.selection[0].Extendable.PlayerId == TeamContext.PlayerTeam)
                {
                    CachedEntity = commandable;
                }
                else
                    CachedEntity = null;
            }
        }
    }

}
