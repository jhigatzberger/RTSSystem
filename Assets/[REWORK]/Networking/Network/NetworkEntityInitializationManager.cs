using System.Collections.Generic;
using Unity.Netcode;
using System.Linq;
using JHiga.RTSEngine;
using JHiga.RTSEngine.Spawning;

namespace JHiga.Networking
{
    public class NetworkEntityInitializationManager : NetworkBehaviour
    {
        private int nextID;
        private List<InitializationData> data;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;
            if (IsServer)
            {
                data = new List<InitializationData>();
                nextID = EntityManager.entities.Count;
                EntityManager.OnRequireEntityID += RequestEntityInitialization;
                LockStep.OnStep += Clear;
            }
        }

        public void Clear()
        {
            data.Clear();
        }

        [ClientRpc]
        private void BroadCastEntityInitializationClientRPC(InitializationData data)
        {
            EntityManager.entities[data.spawnID].GetScriptableComponent<ISpawner>().AuthorizeID(data.entityID);
        }

        public void RequestEntityInitialization(int spawnID)
        {
            int entityID;
            if (data.Any(d => d.spawnID == spawnID)) // OPTIMIZE!
                entityID = data.Find(d => d.spawnID == spawnID).entityID;
            else
                entityID = nextID++;

            BroadCastEntityInitializationClientRPC(new InitializationData()
            {
                spawnID = spawnID,
                entityID = entityID
            });
        }
    }

}
