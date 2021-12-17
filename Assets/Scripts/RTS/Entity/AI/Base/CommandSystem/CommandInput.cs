using RTS.Entity.Selection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    public class CommandInput : MonoBehaviour
    {
        public Command contextCommand;
        private AIEntity cachedEntity;
        private bool shouldClearQueueOnInput = true;
        public void SetClearQueueOnInput(bool shouldClearQueueOnInput)
        {
            this.shouldClearQueueOnInput = shouldClearQueueOnInput;
        }
        public void OnInput()
        {
            if (cachedEntity == null || contextCommand == null)
                return;
            CommandData command = contextCommand.Build(cachedEntity);
            foreach(AIEntity entity in Context.entities)
            {
                if (shouldClearQueueOnInput)
                    entity.ClearCommands();

                CommandContext.EnqueueCommand(
                    new DistributedCommand
                    {
                        data = command,
                        entity = entity.id
                    }
                );
            }
        }
        private void Update()
        {
            CacheEntity();
            if (cachedEntity != null)
                contextCommand = cachedEntity.FirstApplicableCommand;
            else
                contextCommand = null;
        }

        private void CacheEntity()
        {
            if (Context.entities.Count == 0)
                cachedEntity = null;
            else if (cachedEntity != Context.entities[0])
            {
                if (Context.entities[0] is AIEntity)
                    cachedEntity = (AIEntity)Context.entities[0];
                else
                    cachedEntity = null;
            }
        }
<<<<<<< Updated upstream:Assets/Scripts/RTS/Entity/AI/Base/CommandSystem/CommandDistributor.cs
        public void DistributeCommand(DistributedCommand command) // one server sided one, one client sided one
        {
            foreach(int id in command.entities)
            {
                BaseEntity entity = EntityContext.entities[id];
                if (entity != null && entity is AIEntity aiEntity)
                    aiEntity.Enqueue(command.data);
            }
        }
=======
>>>>>>> Stashed changes:Assets/Scripts/RTS/Entity/AI/Base/CommandSystem/CommandInput.cs
    }
    public struct DistributedCommand : IEquatable<DistributedCommand>
    {
        public CommandData data;
        public int entity;

        public bool Equals(DistributedCommand other)
        {
            return other.data.Equals(data) && entity == other.entity;
        }
    }
}
