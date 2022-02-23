using JHiga.RTSEngine.CommandPattern;
using UnityEngine;

namespace JHiga.RTSEngine.SinglePlayer
{
    public class SinglePlayerCommand : MonoBehaviour
    {
        private void Awake()
        {
            CommandEvents.OnCommandDistributionRequested += DistributeCommand;
        }
        public void OnDestroy()
        {
            CommandEvents.OnCommandDistributionRequested -= DistributeCommand;
        }
        private void DistributeCommand(SkinnedCommand command)
        {
            ResolvedCommand.Enqueue(command);
        }       
    }
}