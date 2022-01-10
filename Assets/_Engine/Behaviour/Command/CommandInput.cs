using JHiga.RTSEngine.Selection;
using System;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandInput : MonoBehaviour
    {
        public static CommandInput Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private CommandProperties _contextCommand;
        public CommandProperties ContextCommand
        {
            get => _contextCommand;
            set
            {
                if(value != _contextCommand)
                {
                    _contextCommand = value;
                    OnContextCommand?.Invoke(value);
                }
            }
        }
        private CommandProperties _forcedCommand;
        public CommandProperties ForcedCommand
        {
            get => _forcedCommand;
            set
            {
                if(value != _forcedCommand)
                {
                    SelectionContext.locked = value != null;

                    _forcedCommand = value;
                    OnForcedCommand?.Invoke(value);
                }
            }
        }

        public event Action<CommandProperties> OnContextCommand;
        public event Action<CommandProperties> OnForcedCommand;
        public event Action<ICommandable> OnCachedEntityChanged;
        private ICommandable _cachedEntity;
        public ICommandable CachedEntity
        {
            get { return _cachedEntity; }
            set
            {
                if (value != _cachedEntity)
                {
                    _cachedEntity = value;
                    OnCachedEntityChanged?.Invoke(value);
                }
            }
        }
        private bool shouldClearQueueOnInput = true;
        public void SetClearQueueOnInput(bool shouldClearQueueOnInput)
        {
            this.shouldClearQueueOnInput = shouldClearQueueOnInput;
        }
        public void RequestContextCommand()
        {
            print("RequestContextCommand");
            if (ContextCommand == null || ForcedCommand != null)
                return;
            print("RequestContextCommand ay");
            CommandEvents.RequestCommandDistribution(CommandDistributor.DistributableCommandFromSelectionContext(ContextCommand, shouldClearQueueOnInput));
        }
        public void RequestForcedCommand()
        {
            if (ForcedCommand == null)
                return;
            if(ForcedCommand.Applicable(CachedEntity))
                CommandEvents.RequestCommandDistribution(CommandDistributor.DistributableCommandFromSelectionContext(ForcedCommand, shouldClearQueueOnInput));
            ClearForcedCommand();
        }
        public void ClearForcedCommand()
        {
            ForcedCommand = null;
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
                if (SelectionContext.selection[0].Extendable.TryGetScriptableComponent(out ICommandable commandable) && SelectionContext.selection[0].Extendable.PlayerId == PlayerContext.PlayerId)
                    CachedEntity = commandable;
                else
                    CachedEntity = null;
            }
        }
    }

}
