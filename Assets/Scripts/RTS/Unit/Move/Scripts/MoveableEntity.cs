using RTSEngine.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BaseEntity))]
public class MoveableEntity : MonoBehaviour, IMovable
{
    BaseEntity _entity;
    public BaseEntity Entity => _entity;
    private Queue<Destination> destinations = new Queue<Destination>();
    private NavMeshAgent agent;
    private Destination _currentDestination;
    public Destination Destination { get => _currentDestination;
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
            if(_following != value)
            {
                _following = value;
                if(value)
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
    public void Enqueue(BaseEntity entity)
    {
        Enqueue(new Follow { entity = entity });
    }

    private void Enqueue(Destination destination)
    {
        destinations.Enqueue(destination);
        if (destinations.Count>0)
            Move();
    }

    public void UpdateDestination()
    {
        if (!agent.isOnNavMesh)
            return;
        if(Destination == null)
        {
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            Entity.GetComponent<Animator>().SetFloat("Velocity", 0);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(Destination.Point);
            Entity.GetComponent<Animator>().SetFloat("Velocity", 1);
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

    private void Awake()
    {
        _entity = GetComponent<BaseEntity>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Stop()
    {                
        Destination = null;            
    }

    public void OnExitScene()
    {
        Clear();
        enabled = false;
    }
}

