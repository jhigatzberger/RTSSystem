using JHiga.RTSEngine.Selection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandDistributor : MonoBehaviour
    {
        private static CommandDistributor instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            CommandEvents.OnCommandEnqueueRequested += DistributeCommand;
        }
        private void OnDestroy()
        {
            CommandEvents.OnCommandEnqueueRequested -= DistributeCommand;
        }
        private void DistributeCommand(DistributableCommand command)
        {
            foreach (int entity in command.entities)
            {
                IExtendableInteractable gameEntity = InteractableRegistry.entities[entity];
                if (gameEntity != null && gameEntity.TryGetScriptableComponent(out ICommandable commandable))
                {
                    if (command.clearQueueOnEnqeue)
                        commandable.Clear();
                    commandable.Enqueue(command.data);
                }
            }
        }
        public static DistributableCommand DistributableCommandFromSelectionContext(CommandProperties command, bool shouldClearQueueOnInput)
        {
            int playerTeam = PlayerContext.PlayerId;
            List<int> entites = new List<int>();
            foreach (ISelectable selectable in SelectionContext.selection)
            {
                if (selectable.Extendable.PlayerId == playerTeam)
                    entites.Add(selectable.Extendable.EntityId);
            }
            return
                new DistributableCommand
                {
                    data = command.Compile(),
                    clearQueueOnEnqeue = shouldClearQueueOnInput,
                    entities = entites.ToArray()
                };
        }
    }
}