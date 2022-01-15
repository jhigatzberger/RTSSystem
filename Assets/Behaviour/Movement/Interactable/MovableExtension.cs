using JHiga.RTSEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JHiga.RTSEngine.Movement
{
    public class MovableExtension : BaseInteractableExtension<MovableProperties>, IMovable
    {
        public MovableExtension(IExtendableEntity entity, MovableProperties properties) : base(entity, properties)
        {
            agent = Entity.MonoBehaviour.GetComponent<NavMeshAgent>();
        }

        private Queue<Target> destinations = new Queue<Target>();
        private NavMeshAgent agent;
        private Target? _currentDestination;
        public Target? Destination
        {
            get => _currentDestination;
            set
            {
                _currentDestination = value;
                UpdateDestination();
                if (value?.entity != null)
                    LockStep.OnStep += UpdateDestination;
            }
        }

        public float Distance => Destination.HasValue? Destination.Value.Distance(Entity.MonoBehaviour.transform.position) : 0;
        public void Enqueue(Target destination)
        {
            destinations.Enqueue(destination);
            if (destinations.Count > 0)
                Move();
        }
        public void UpdateDestination()
        {
            if (!agent.isOnNavMesh)
                return;
            if (Destination == null)
            {
                agent.SetDestination(Entity.MonoBehaviour.transform.position);
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(Destination.Value.entity == null ? Destination.Value.position : Destination.Value.entity.MonoBehaviour.transform.position);
            }
        }
        public void Move()
        {
            if (destinations.Count == 0)
                return;
            Destination = destinations.Dequeue();
        }
        public override void Clear()
        {
            destinations.Clear();
            Stop();
        }
        public void Stop()
        {
            Destination = null;
        }
        public override void Enable()
        {
            agent.speed = Properties.movementSpeed;
        }
        public override void Disable()
        {
            base.Disable();
            Entity.MonoBehaviour.GetComponent<NavMeshAgent>().enabled = false;
        }
    }


}
