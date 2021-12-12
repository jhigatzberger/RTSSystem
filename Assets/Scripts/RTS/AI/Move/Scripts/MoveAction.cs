using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.AI
{
    [CreateAssetMenu(fileName = "NavMeshMove", menuName = "RTS/AI/NavMeshMove")]
    public class MoveAction : Action
    {
        public override void Enter(AIEntity entity)
        {
            entity.GetComponent<Animator>().SetFloat("Velocity", 1);
            Debug.Log("ENTER!");
            MovableAIEntityExtension movable = entity.GetComponent<MovableAIEntityExtension>();
            CommandData data = entity.currentCommand.Value;
            if (data.target != null)
                movable.Follow(data.target.transform);
            else
                movable.Move(data.position.Value);
        }

        public override void Exit(AIEntity entity)
        {
            Debug.Log("EXIT!");
            entity.GetComponent<Animator>().SetFloat("Velocity", 0);
            MovableAIEntityExtension movable = entity.GetComponent<MovableAIEntityExtension>();
            movable.Clear();
        }
    }
}
