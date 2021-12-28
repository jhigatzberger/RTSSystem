using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace RTSEngine.Networking
{
    public class SyncedNetworkActionManager : NetworkBehaviour
    {
        private static Dictionary<uint, SyncedNetworkAction> actions = new Dictionary<uint, SyncedNetworkAction>();
        [SerializeField] private SyncedNetworkAction[] _actions;
        private void Awake()
        {
            foreach (SyncedNetworkAction action in _actions)
                actions.Add(action.Reg_ID,action);
            LockStep.OnStep += LockStep_OnStep;
        }

        private void LockStep_OnStep()
        {

        }

        [ServerRpc]
        public void ConfirmActionServerRPC(SyncedNetworkActionData data, ulong clientID)
        {

        }
        [ClientRpc]
        public void ConfirmActionClientRpc(SyncedNetworkActionData data, ulong clientID)
        {

        }

        private static List<SyncedNetworkActionData> toTriggerOnce = new List<SyncedNetworkActionData>();
        public static void TriggerOnce(uint actionID, ulong entityID)
        {
            toTriggerOnce.Add(new SyncedNetworkActionData
            {
                actionID = actionID,
                entityID = entityID
            });
        }
        private static List<SyncedNetworkActionData> toTriggerRepeating = new List<SyncedNetworkActionData>();
        public static void TriggerRepeating(uint actionID, ulong entityID)
        {
            toTriggerRepeating.Add(new SyncedNetworkActionData
            {
                actionID = actionID,
                entityID = entityID
            });
        }
    }

    public struct SyncedNetworkActionData
    {
        public uint actionID;
        public ulong entityID;
    }

}
