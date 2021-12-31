using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSEngine.Core.Movement
{
    public class MovableExtension : RTSExtension, IMovable
    {
        public MovableExtension(RTSBehaviour behaviour) : base(behaviour)
        {
            agent = Behaviour.GetComponent<NavMeshAgent>();
            Behaviour.OnClear += Clear;
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
        public void Enqueue(RTSBehaviour entity)
        {
            Enqueue(new Follow { entity = entity });
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
                agent.SetDestination(Behaviour.transform.position);
                agent.isStopped = true;
                Behaviour.GetComponent<Animator>().SetFloat("Velocity", 0);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(Destination.Point);
                Behaviour.GetComponent<Animator>().SetFloat("Velocity", 1);
            }
        }
        public void Move()
        {
            if (destinations.Count == 0)
                return;
            Destination = destinations.Dequeue();
        }
        public void Clear()
        {
            destinations.Clear();
            Stop();
        }
        public void Stop()
        {
            Destination = null;
        }
        protected override void OnExitScene()
        {
            Clear();
            Behaviour.GetComponent<NavMeshAgent>().enabled = false;
            Behaviour.OnClear -= Clear;
        }
    }


}
