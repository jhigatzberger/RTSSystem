using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Entity
{
    [RequireComponent(typeof(BaseEntity))]
    public class MoveableEntity : MonoBehaviour, IMovable
    {
        BaseEntity _entity;
        public BaseEntity Entity => _entity;
        private Queue<Vector3> destinations = new Queue<Vector3>();
        private NavMeshAgent agent;
        private Vector3? currentDestination;
        public void Enqueue(Vector3 destination)
        {
            destinations.Enqueue(destination);
            if (!currentDestination.HasValue)
                Move();
        }
        public Vector3? Destination { get; set; }

        public void Move()
        {
            agent.isStopped = false;
            if (destinations.Count == 0)
                return;
            Entity.GetComponent<Animator>().SetFloat("Velocity", 1);
            Destination = destinations.Dequeue();
            agent.SetDestination(Destination.Value);
        }

        public void Clear()
        {
            destinations.Clear();
            Stop();
        }

        private void Awake()
        {
            _entity = GetComponent<BaseEntity>();
            agent = GetComponent<NavMeshAgent>();
        }

        public void Stop()
        {
            Entity.GetComponent<Animator>().SetFloat("Velocity", 0);
            agent.isStopped = true;
            currentDestination = null;
            agent.SetDestination(transform.position);
        }
    }

}
