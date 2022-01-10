using JHiga.RTSEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JHiga.RTSEngine.Movement
{
    public class MovableExtension : BaseInteractableExtension<MovableProperties>, IMovable
    {
        public MovableExtension(IExtendableInteractable entity, MovableProperties properties) : base(entity, properties)
        {
            agent = Extendable.MonoBehaviour.GetComponent<NavMeshAgent>();
        }

        private Queue<Destination> destinations = new Queue<Destination>();
        private NavMeshAgent agent;
        private Destination _currentDestination;
        public Destination Destination
        {
            get => _currentDestination;
            set
            {
                _currentDestination = value;
                Following = value != null && value is Follow;
                UpdateDestination();
            }
        }
        bool _following = false;
        public bool Following
        {
            get => _following;
            set
            {
                if (_following != value)
                {
                    _following = value;
                    if (value)
                        LockStep.OnStep += UpdateDestination;
                    else
                        LockStep.OnStep -= UpdateDestination;
                }
            }
        }
        public void Enqueue(Vector3 destination)
        {
            Enqueue(new Destination { point = destination });
        }
        public void Enqueue(Transform follow)
        {
            Enqueue(new Follow { toFollow = follow });
        }
        private void Enqueue(Destination destination)
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
                agent.SetDestination(Extendable.MonoBehaviour.transform.position);
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(Destination.Point);
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
            Extendable.MonoBehaviour.GetComponent<NavMeshAgent>().enabled = false;
        }
    }


}
