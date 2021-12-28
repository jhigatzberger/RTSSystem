using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

namespace RTSEngine.Entity
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
                nextID = EntityContext.entities.Count;
                EntityContext.OnRequireEntityID += RequestEntityInitialization;
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
            EntityContext.entities[data.spawnID].GetComponent<ISpawner>().AuthorizeID(data.entityID);
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
