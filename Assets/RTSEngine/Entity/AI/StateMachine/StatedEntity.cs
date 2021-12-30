using RTSEngine.Entity;
using RTSEngine.Entity.AI;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BaseEntity))]
public class StatedEntity : MonoBehaviour, IStateMachine
{
    [SerializeField] private State currentState;
    [SerializeField] private State defaultState;
    public float TimeStamp { get; set; }
    public BaseEntity Entity { get; set; }

    public UnityEvent OnStateChainEnd => _onStateChainEnd;
    [SerializeField] private UnityEvent _onStateChainEnd;
    public void ChangeState(State state)
    {
        if (state == currentState)
            return;
        if (currentState != null)
            currentState.Exit(this);
        currentState = state;
        if (currentState != null)
            currentState.Enter(this);
        else
            OnStateChainEnd.Invoke();
        TimeStamp = LockStep.time;
    }

    private void OnEnable()
    {
        LockStep.OnStep += UpdateState;
        Entity.OnClear += ResetState;
    }

    public void ResetState()
    {
        ChangeState(null);
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.CheckTransitions(this);
        else if (defaultState != null)
            ChangeState(defaultState);
    }

    public void OnExitScene()
    {
        Debug.LogWarning(Entity.id + " died");
        Entity.OnClear -= ResetState;
        LockStep.OnStep -= UpdateState;
        if(currentState != null)
            currentState.Exit(this);
        enabled = false;
    }
}


