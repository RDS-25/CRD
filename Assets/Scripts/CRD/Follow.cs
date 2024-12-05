
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public Transform[] wayPoints;
    private NavMeshAgent agent;
    public int currentIndex = 0;
    

    private void Start()
    {
        FindWayPoint();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        if (wayPoints.Length > 0)
        {
            agent.SetDestination(wayPoints[currentIndex].position);
        }
    }


    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // 다음 웨이포인트로 이동하도록 인덱스를 갱신합니다.
            currentIndex = (currentIndex + 1) % wayPoints.Length; // 자동으로 순환합니다.
            agent.SetDestination(wayPoints[currentIndex].position);
        }
    }
    
    void FindWayPoint()
    {
        // WayPoint 게임 오브젝트를 찾습니다.
        GameObject wayPointParent = GameObject.Find("PatrolGroups");

        if (wayPointParent != null)
        {
            // GetComponentsInChildren을 사용하여 자식 오브젝트들을 Transform 배열로 가져옵니다.
            wayPoints = wayPointParent.GetComponentsInChildren<Transform>();
            
            // 필요하다면 첫 번째 요소(부모 자신)를 제외하고 작업을 수행합니다.
            wayPoints = wayPoints.Skip(1).ToArray();
        }
        else
        {
            Debug.LogError("WayPoint object not found!");
        }
    }
}