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
            // 여기서 공격 루틴을 커맨드 안에 두고 있는데,
            // 원래 command 패턴에서는 꼭 그런 것보다는
            // 그 command를 실행하기 위한 루틴을 알고 그걸 실행하는 것이 보통.
            // 그러나 여기서는 command를 컴포넌트로 붙일 예정이므로
            // 여기에 공격 루틴을 만들도록 한다.
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
                // 거리 안에 있으면 공격
                StateMachine machine = GetComponent<StateController>().stateMachine;
                machine.TransitionTo(machine.attackState);
                PerformAttack(unit, target);
                yield return new WaitForSeconds(1);
            }
            else
            {
                // 거리 밖에 있으면 다가가자
                StateMachine machine = GetComponent<StateController>().stateMachine;
                machine.TransitionTo(machine.runState);
                GetComponent<NavMeshAgent>().SetDestination((target as MonoBehaviour).transform.position);
                while (!IsInRange(unit,target))
                {
                    // if (!IsTargetValid()) 
                    //     yield break;
                    yield return null;
                    // 여기서 타겟이 도망갔을 경우, setdestination을 하는 부분이
                    // 필요함. 안그러면 못쫓아감
                    // 만약 pendingpath가 끝나고 remaindistance가 계산 가능할 때
                    // 또는 원하는 목적지에 도달했을 때
                    // 검증해서 setdestination을 다시 수행하면 됨.
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
        Debug.Log(unit.name + "이 " + (target as MonoBehaviour).name + " 공격 중!");
        // 공격 계산할거 있으면 여기서 하면 됨
    }
}
