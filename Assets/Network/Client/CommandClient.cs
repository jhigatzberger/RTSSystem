using JHiga.RTSEngine.CommandPattern;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class CommandClient : MonoBehaviour
    {
        #region Initialization
        public static CommandClient Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            LockStep.OnStep += LockStep_OnStep;
            CommandEvents.OnCommandDistributionRequested += DistributeCommand;
        }


        public void OnDestroy()
        {
            CommandEvents.OnCommandDistributionRequested -= DistributeCommand;
            LockStep.OnStep -= LockStep_OnStep;
        }
        #endregion
        
        private readonly Queue<ScheduledCommand> scheduledCommands = new Queue<ScheduledCommand>();
        private void DistributeCommand(SkinnedCommand command)
        {
            Debug.Log("distrib");
            CommandNetwork.Instance.AddCommandServerRPC(command);
        }
        public void AddCommand(ulong commandID, ulong clientID)
        {
            Debug.Log("confirm");
            CommandNetwork.Instance.ConfirmCommandServerRPC(commandID, clientID);
        }
        public void Schedule(ScheduledCommand command)
        {
            scheduledCommands.Enqueue(command);
        }
        private void LockStep_OnStep()
        {
            while (scheduledCommands.Count > 0 && scheduledCommands.Peek().scheduledStep == LockStep.count)
                new ResolvedCommand(scheduledCommands.Dequeue().command).Enqueue();
            if (scheduledCommands.Count > 0 && scheduledCommands.Peek().scheduledStep < LockStep.count)
                Debug.LogError("Desync ya fakin monkey");
        }
    }
}