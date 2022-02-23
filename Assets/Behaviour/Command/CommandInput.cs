using JHiga.RTSEngine.InputHandling;
using JHiga.RTSEngine.Selection;
using System;
using System.Linq;
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
        public GameObject commandPreview;
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

                    if (commandPreview != null)
                        Destroy(commandPreview.gameObject);

                    if (value != null && value.forcedPreview != null)
                        commandPreview = Instantiate(value.forcedPreview);
                    else
                        commandPreview = null;
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
        public bool shouldClearQueueOnInput = true;
        public void SetClearQueueOnInput(bool shouldClearQueueOnInput)
        {
            this.shouldClearQueueOnInput = shouldClearQueueOnInput;
        }

        public void RequestContextCommand()
        {
            if (ContextCommand == null || ForcedCommand != null)
                return;
            ContextCommand.Request(CachedEntity, SelectionContext.selection.Select(s => s.Entity).ToArray(), shouldClearQueueOnInput, CommandEvents.RequestCommandDistribution);
        }
        public void RequestForcedCommand()
        {
            if (ForcedCommand == null)
                return;
            if(ForcedCommand.IsApplicable(CachedEntity, true))
                ForcedCommand.Request(CachedEntity, SelectionContext.selection.Select(s => s.Entity).ToArray(), shouldClearQueueOnInput, CommandEvents.RequestCommandDistribution);
            ClearForcedCommand();
        }
        public void ClearForcedCommand()
        {
            ForcedCommand = null;
        }
        private void Update()
        {
            if (commandPreview != null && InputManager.worldPointerPosition.HasValue)
                commandPreview.transform.position = InputManager.worldPointerPosition.Value;
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
            else if (CachedEntity == null || CachedEntity.Entity != SelectionContext.selection[0].Entity)
            {
                if (SelectionContext.selection[0].Entity.TryGetExtension(out ICommandable commandable) && SelectionContext.selection[0].Entity.UID.player == PlayerContext.PlayerId)
                    CachedEntity = commandable;
                else
                    CachedEntity = null;
            }
        }
    }

}
