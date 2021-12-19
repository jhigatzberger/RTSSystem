using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    public class StateMachineManager : MonoBehaviour
    {
        public static HashSet<StatedEntity> machines = new HashSet<StatedEntity>();

        [SerializeField] private float ticksPerSecond = 2;

        private static StateMachineManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        private float timeSinceLastTick = 1000;
        private void Update()
        {
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick > 1 / ticksPerSecond)
                UpdateStates();
        }

        private void UpdateStates()
        {
            timeSinceLastTick = 0;
            foreach(StatedEntity machine in machines)
                machine.UpdateState();
        }
    }

}
