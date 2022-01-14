using System;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    /// <summary>
    /// Allows decoupling from Networked Command handling.
    /// </summary>
    public static class CommandEvents
    {
        public static event Action<SkinnedCommand> OnCommandDistributionRequested;
        public static event Action<SkinnedCommand> OnCommandEnqueueRequested;

        /// <summary>
        /// Requests a command to be distributed to all clients.
        /// The first step after issuing a command input through <see cref="CommandInput"/>.
        /// </summary>
        /// <param name="command">Packed by <see cref="CommandDistributor.DistributableCommandFromSelectionContext(CommandProperties, bool)"/></param>
        public static void RequestCommandDistribution(SkinnedCommand command)
        {
            OnCommandDistributionRequested?.Invoke(command);
        }
        /// <summary>
        /// Requests a command to be enqueued locally on all entities that are concerned by it.
        /// </summary>
        /// <param name="command">The distributed command e.g. from <see cref="NetworkCommandClient"/></param>
        public static void RequestCommandEnqueue(SkinnedCommand command)
        {
            OnCommandEnqueueRequested?.Invoke(command);
        }
    }

}
