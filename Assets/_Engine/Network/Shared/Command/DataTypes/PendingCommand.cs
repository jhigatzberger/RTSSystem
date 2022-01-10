using JHiga.RTSEngine.CommandPattern;
using System.Collections.Generic;

namespace JHiga.RTSEngine.Network
{
    public class PendingCommand
    {
        public DistributableCommand command;
        public List<ulong> pendingClients;

        public bool Confirm(ulong clientID)
        {
            pendingClients.Remove(clientID);
            return pendingClients.Count == 0;
        }
    }
}
