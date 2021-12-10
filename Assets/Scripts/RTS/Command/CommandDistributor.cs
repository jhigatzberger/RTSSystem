using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class CommandDistributor : MonoBehaviour
    {
        public void OnInput()
        {
            foreach(EntityController entity in Context.Selection.items)
            {
                if(entity is ICommandable)
                {
                    ICommandable commandable = (ICommandable)entity;
                    commandable.StopAndClear();
                    Command command = commandable.CreateCommandFromContext();
                    if(command != null)
                        commandable.Enqueue(command);
                }
            }
        }
    }

}
