using RTS.Entity.Selection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity
{
    public class CommandInput : MonoBehaviour
    {
        public Command _contextCommand;
        public Command ContextCommand
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
        public static event Action<int> OnContextCommand;
        private ICommandable cachedEntity;
        public bool shouldClearQueueOnInput = true;
        public void SetClearQueueOnInput(bool shouldClearQueueOnInput)
        {
            this.shouldClearQueueOnInput = shouldClearQueueOnInput;
        }
        public void OnInput()
        {
            if (ContextCommand == null)
                return;
            foreach(ICommandable commandable in Context.entities)
            {
                CommandContext.EnqueueCommand(
                    new DistributedCommand
                    {
                        data = ContextCommand.Build(commandable),
                        clearQueueOnEnqeue = shouldClearQueueOnInput,
                        entity = commandable.Entity.id
                    }
                );
            }
        }
        private void Update()
        {
            CacheEntity();
            if (cachedEntity != null)
                ContextCommand = cachedEntity.FirstApplicableCommand;
            else
                ContextCommand = null;
        }

        private void CacheEntity()
        {
            if (Context.entities.Count == 0)
                cachedEntity = null;
            else if (cachedEntity == null || cachedEntity.Entity != Context.entities[0])
            {
                if (Context.entities[0] is ICommandable)
                    cachedEntity = (ICommandable)Context.entities[0];
                else
                    cachedEntity = null;
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