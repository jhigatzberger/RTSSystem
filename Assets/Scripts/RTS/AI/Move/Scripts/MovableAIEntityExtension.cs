using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovableAIEntityExtension : AIEntityExtension
    {
        [HideInInspector] public NavMeshAgent agent;
        public override int Priority => 0;
        public override bool Applicable => InputManager.worldPointerPosition.HasValue || EntityContext.FirstOrNullHovered != null;
        [SerializeField] Command _moveCommand;
        public override Command Command => _moveCommand;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        public override CommandData ContextPoll()
        {
            return new CommandData { command = Command, target = EntityContext.FirstOrNullHovered, position = InputManager.worldPointerPosition };
        }

        private Transform toFollow = null;
        public void Follow(Transform transform)
        {
            toFollow = transform;
        }
        public void Move(Vector3 position)
        {
            agent.SetDestination(position);
        }
        public void Clear()
        {
            toFollow = null;
            agent.SetDestination(transform.position);
        }

        private void FixedUpdate()
        {
            if(toFollow != null)
                agent.SetDestination(toFollow.position);
        }
    }

}
