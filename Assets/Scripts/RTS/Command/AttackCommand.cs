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
            // ���⼭ ���� ��ƾ�� Ŀ�ǵ� �ȿ� �ΰ� �ִµ�,
            // ���� command ���Ͽ����� �� �׷� �ͺ��ٴ�
            // �� command�� �����ϱ� ���� ��ƾ�� �˰� �װ� �����ϴ� ���� ����.
            // �׷��� ���⼭�� command�� ������Ʈ�� ���� �����̹Ƿ�
            // ���⿡ ���� ��ƾ�� ���鵵�� �Ѵ�.
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
                // �Ÿ� �ȿ� ������ ����
                StateMachine machine = GetComponent<StateController>().stateMachine;
                machine.TransitionTo(machine.attackState);
                PerformAttack(unit, target);
                yield return new WaitForSeconds(1);
            }
            else
            {
                // �Ÿ� �ۿ� ������ �ٰ�����
                StateMachine machine = GetComponent<StateController>().stateMachine;
                machine.TransitionTo(machine.runState);
                GetComponent<NavMeshAgent>().SetDestination((target as MonoBehaviour).transform.position);
                while (!IsInRange(unit,target))
                {
                    // if (!IsTargetValid()) 
                    //     yield break;
                    yield return null;
                    // ���⼭ Ÿ���� �������� ���, setdestination�� �ϴ� �κ���
                    // �ʿ���. �ȱ׷��� ���Ѿư�
                    // ���� pendingpath�� ������ remaindistance�� ��� ������ ��
                    // �Ǵ� ���ϴ� �������� �������� ��
                    // �����ؼ� setdestination�� �ٽ� �����ϸ� ��.
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
        // ���� ����Ұ� ������ ���⼭ �ϸ� ��
    }
}
