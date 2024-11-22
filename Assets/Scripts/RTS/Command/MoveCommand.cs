using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : MonoBehaviour, ICommand
{
    public string Name => "Move";

    Vector3 destination;
    NavMeshAgent agent;

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    public bool CanExecute(ISelectable executor)
    {
        return GetComponent<NavMeshAgent>() != null;
    }

    public void Execute(ISelectable executor)
    {
        if (executor is Unit unit && GetComponent<NavMeshAgent>() is NavMeshAgent agent)
        {
            this.agent = agent;
            StateMachine stateMachine = GetComponent<StateController>().stateMachine;
            stateMachine.TransitionTo(stateMachine.runState);
            agent.SetDestination(destination);
            unit.StartCoroutine(WaitForArrival());
        }
    }

    IEnumerator WaitForArrival()
    {
        while (agent.pathPending)
        {
            yield return null;
        }
        while (agent.remainingDistance> agent.stoppingDistance)
        {
            yield return null;
        }
        StateMachine stateMachine = GetComponent<StateController>().stateMachine;
        stateMachine.TransitionTo(stateMachine.idleState);
    }

    public void Cancel()
    {
        if (agent != null)
        {
            agent.ResetPath();
        }
    }
}
