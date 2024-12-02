using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelectable
{
    public List<Transform> potentialTargets; // ������ Ÿ�� ���
    public UnitData unitData;
    [SerializeField] GameObject selectIndicator;

    bool isSelected = false;
    float attackCooldown = 0f;

    ICommand currentCommand;
    private NavMeshAgent agent;
    public float attackRange;
    public float attackSpeed;
    public float hp;
    public float mp;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = unitData.moveSpeed;
        attackRange = unitData.attackRange;
        hp = unitData.health;
        mp = unitData.magicPoint;
        attackSpeed = unitData.attackSpeed;
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime; // ���� ��ٿ��� ���ҽ�Ŵ

        FindEnemiesInRange();

        Transform target = FindClosestTargetInRange();

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                if (attackCooldown <= 0f)
                {
                    Attack(target);
                    attackCooldown = 1f / attackSpeed; // ���� �ӵ��� ���� ��ٿ� ����
                }
            }
            else
            {
                //������ ������� ���󰡱�
                MoveTowardsTarget(target);
            }
        }
    }

    void FindEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        potentialTargets.Clear(); // ���� Ÿ�� ��� �ʱ�ȭ

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                potentialTargets.Add(hitCollider.transform);
            }
        }
    }

    Transform FindClosestTargetInRange()
    {
        Transform closestTarget = null;
        float closestDistance = attackRange;

        foreach (Transform potentialTarget in potentialTargets)
        {
            if (potentialTarget.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, potentialTarget.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = potentialTarget;
                }
            }
        }

        return closestTarget;
    }

    void MoveTowardsTarget(Transform target)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target.position, out hit, attackRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void Attack(Transform target)
    {
        for (int i = 0; i < unitData.skills.Length; i++)
        {
            target.GetComponent<PropertyDisplay>().currenthp -=  UseSkill(i);
            
        }
        
        transform.LookAt(target);
    }
    //��ų ���
    float UseSkill(int skillindex)
    {
        if (skillindex < 0 || skillindex >= unitData.skills.Length)
            return 0;
        SkillData skillData = unitData.skills[skillindex];
        Debug.Log(skillData.skillName+":"+skillData.ApplySkill(unitData));
        return skillData.ApplySkill(unitData);
        
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void Select()
    {
        isSelected = true;
        selectIndicator.SetActive(true);
    }

    public void Deselect()
    {
        isSelected = false;
        selectIndicator.SetActive(false);
    }

    public void ExecuteCommand(ICommand command)
    {
        if (currentCommand != null)
        {
            currentCommand.Cancel();
            StopAllCoroutines();
        }

        if (command.CanExecute(this))
        {
            currentCommand = command;
            command.Execute(this);
        }
        else
        {
            // �ൿ �ȵɶ� 
        }
    }
}