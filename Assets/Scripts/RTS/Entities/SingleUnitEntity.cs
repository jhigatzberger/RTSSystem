using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using RTS.Command;

namespace RTS
{
    public class SingleUnitEntity : BaseEntity, ICommandable, IMovable
    {
        public override int Priority => 10;
        
        #region Commandable        
        Queue<BaseCommand> commandQueue = new Queue<BaseCommand>();
        public BaseCommand Current { get; set; }
        public void Enqueue(BaseCommand command)
        {
            commandQueue.Enqueue(command);
            if (Current == null || !Current.running)
                ExecuteFirst();
        }
        public void ExecuteFirst()
        {
            if(commandQueue.Count == 0)
                return;
            Current = commandQueue.Dequeue();
            Current.Execute();
        }

        public void StopAndClear()
        {
            Current?.Stop();
            Current = null;
            commandQueue.Clear();
        }

        public Type[] AvailableCommands()
        {
            return new Type[] { typeof(MoveCommand) };
        }

        public BaseCommand CreateCommandFromContext(int index)
        {
            if (!InputManager.worldPointerPosition.HasValue)
                return null;
            return new MoveCommand(this, this, Formation.Context.current.GetPosition(InputManager.worldPointerPosition.Value, index, Selection.Context.Count));
        }
        #endregion

        #region Movable
        // this functionality will partially be moved into the state machine!
        private IEnumerator moveCoroutine;
        public IEnumerator MoveToAsync(MoveCommand command)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(command.destination);
            while (Vector3.Distance(transform.position, command.destination) > 0.1f + agent.stoppingDistance)
                yield return null;
            command.Finish();
        }
        public void MoveTo(MoveCommand command)
        {
            moveCoroutine = MoveToAsync(command);
            StartCoroutine(moveCoroutine);
        }
        public void Stop()
        {
            StopCoroutine(moveCoroutine);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(transform.position);
        }
        #endregion

        protected override void OnExitScene()
        {
            base.OnExitScene();
            StopAndClear();
        }
    }

}
