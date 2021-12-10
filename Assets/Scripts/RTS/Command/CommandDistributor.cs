using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Command
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
            foreach(BaseEntity entity in Context.Selection.items)
            {
                if(entity is ICommandable)
                {
                    ICommandable commandable = (ICommandable)entity;
                    if(shouldClearQueueOnInput)
                        commandable.StopAndClear();
                    BaseCommand command = commandable.CreateCommandFromContext();
                    if(command != null)
                        commandable.Enqueue(command);
                }
            }
        }
    }

}
