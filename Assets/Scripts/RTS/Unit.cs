using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelectable
{
    public List<Transform> potentialTargets; // 잠재적 타겟 목록
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
        attackCooldown -= Time.deltaTime; // 공격 쿨다운을 감소시킴

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
                    attackCooldown = 1f / attackSpeed; // 공격 속도에 따라 쿨다운 설정
                }
            }
            else
            {
                //없으면 가까운적 따라가기
                MoveTowardsTarget(target);
            }
        }
    }

    void FindEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        potentialTargets.Clear(); // 이전 타겟 목록 초기화

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
    //스킬 사용
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
            // 행동 안될때 
        }
    }
}