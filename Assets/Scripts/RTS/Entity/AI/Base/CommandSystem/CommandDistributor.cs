using RTS.Entity.Selection;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    public class CommandDistributor : MonoBehaviour
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
            List<int> ids = new List<int>();
            foreach(AIEntity entity in Context.entities)
            {
                if (shouldClearQueueOnInput)
                    entity.ClearCommands();
                ids.Add(entity.id);
            }
            DistributeCommand(new DistributedCommand
            {
                data = command,
                entities = ids.ToArray()
            });
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
        public void DistributeCommand(DistributedCommand command) // one server sided one, one client sided one
        {
            foreach(int id in command.entities)
            {
                BaseEntity entity = EntityContext.entities[id];
                if (entity != null && entity is AIEntity aiEntity)
                    aiEntity.Enqueue(command.data);
            }
        }
    }
    public struct DistributedCommand
    {
        public CommandData data;
        public int[] entities;
    }
}
