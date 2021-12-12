using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.AI
{
    public class CommandDistributor : MonoBehaviour
    {

        private bool shouldClearQueueOnInput = true;
        public void SetClearQueueOnInput(bool shouldClearQueueOnInput)
        {
            this.shouldClearQueueOnInput = shouldClearQueueOnInput;
        }
        public void OnInput()
        {
            foreach(AIEntity entity in Selection.Context.items)
            {
                if(shouldClearQueueOnInput)
                    entity.StopAndClear();
                CommandData? command = entity.GetCommandFromContext();
                if(command.HasValue)
                    entity.Enqueue(command.Value);
                
            }
        }
    }

}
