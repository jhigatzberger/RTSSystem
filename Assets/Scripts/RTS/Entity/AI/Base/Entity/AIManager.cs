using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    public class AIManager : MonoBehaviour
    {
        public static HashSet<AIEntity> entities = new HashSet<AIEntity>();

        [SerializeField] private float ticksPerSecond = 2;

        private static AIManager instance;
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
                Tick();
        }

        private void Tick() // make this server sided
        {
            timeSinceLastTick = 0;
            foreach(AIEntity entity in entities)
            {
                entity.AIUpdate(); // pass the server time
            }
        }
    }

}
