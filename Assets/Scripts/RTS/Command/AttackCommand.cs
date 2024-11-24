using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackCommand : MonoBehaviour, ICommand
{
    public string Name => "Attack";
    ISelectable target;

    public void SetTarget(ISelectable target)
    {
        this.target = target;
    }

    public bool CanExecute(ISelectable executor)
    {
        if (target == executor)
        {
            return false;
        }
        return target != null && executor is Unit;
    }

    public void Cancel()
    {
    }

    public void Execute(ISelectable executor)
    {
        if (executor is Unit unit && target!=null) 
        {
            unit.StartCoroutine(AttackRoutine(unit));
        }
    }

    IEnumerator AttackRoutine(Unit unit)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        while (true)
        {
            // if (!IsTargetValid()) 
            //     yield break;

            if (IsInRange(unit,target))
            {
                
                StateMachine machine = GetComponent<StateController>().stateMachine;
                machine.TransitionTo(machine.attackState);
                PerformAttack(unit, target);
                yield return new WaitForSeconds(1);
            }
            else
            {
                
                StateMachine machine = GetComponent<StateController>().stateMachine;
                machine.TransitionTo(machine.runState);
                GetComponent<NavMeshAgent>().SetDestination((target as MonoBehaviour).transform.position);
                while (!IsInRange(unit,target))
                {
                    // if (!IsTargetValid()) 
                    //     yield break;
                    yield return null;
               
                }
                GetComponent<NavMeshAgent>().ResetPath();
            }
        }
    }

    bool IsInRange(Unit unit, ISelectable target)
    {
        float distance = Vector3.Distance(unit.transform.position,
            (target as MonoBehaviour).transform.position);
        return distance <= unit.attackRange;
    }

    void PerformAttack(Unit unit, ISelectable target)
    {
        unit.transform.LookAt((target as MonoBehaviour).transform.position);
        Debug.Log(unit.name + "�� " + (target as MonoBehaviour).name + " ���� ��!");
        
    }
}
