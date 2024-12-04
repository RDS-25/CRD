using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    public EnemyData enemyPrefabs;
    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private List<GameObject> poolUnit = new List<GameObject>();
    private Dictionary<int, GameObject> stagePrefabs;
    [SerializeField] private Transform swpanPos;
    [Header("Initialized spawned enemies")]
    [SerializeField] private Transform swpanGroups;
    [SerializeField] private Transform UnitGroup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeDictionary();
            InitializePool();
        }
        else
        {
            Destroy(gameObject); // 중복제거
        }
    }

    private void InitializeDictionary()
    {
        stagePrefabs = new Dictionary<int, GameObject>();

        for (int i = 0; i < enemyPrefabs.enemyProperties.Count; i++)
        {
            stagePrefabs.Add(i + 1, enemyPrefabs.enemyProperties[i].prefab); 
        }
    }
    private void InitializePool()
    {
        var Unitprefab = Resources.LoadAll<GameObject>("Prefabs");
        //유닛 오브젝트 
        foreach (var Unit in Unitprefab)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Unit);
                obj.transform.SetParent(UnitGroup);
                obj.SetActive(false);
                poolUnit.Add(obj);
            }
        }
        //적 오브젝트
        foreach (var property in enemyPrefabs.enemyProperties)
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = Instantiate(property.prefab);
                var enemy = obj.GetComponent<PropertyDisplay>();
                enemy.ammor = property.Armor;
                enemy.speed = property.Speed;
                enemy.maxhp = property.Maxhp;
                obj.transform.position = swpanPos.position;
                obj.transform.SetParent(swpanGroups);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    //적 나오게하기
    public GameObject GetObject(int stageNumber)
    {
        GameObject prefab = stagePrefabs[stageNumber];
        
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(prefab.name))
            {
                obj.transform.position = swpanPos.position;
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab);
        newObj.transform.position = swpanPos.position;
        pool.Add(newObj);
        return newObj;
    }
    
    //아군
    public GameObject GetObjectUnit(GameObject prefab)
    {
        // unitPool 리스트에서 게임 오브젝트가 있는지 확인
        foreach (var obj in poolUnit)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(prefab.name))
            {
                // 오브젝트를 활성화하고 위치를 설정한 뒤 반환
                
                obj.SetActive(true);
                return obj;
            }
        }
        

        GameObject newObj = Instantiate(prefab);
        newObj.transform.position = swpanPos.position;
        poolUnit.Add(newObj);
        newObj.transform.SetParent(UnitGroup);
        return newObj;
    }
  
    

    public void ReturnObject(GameObject obj)
    {
        if (obj.name.Contains("Wisp"))
        {
            obj.SetActive(false);
            return; 
        }
        var enemy = obj.GetComponent<PropertyDisplay>();
        enemy.currenthp = enemy.maxhp;
        enemy.isDead = false;
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        enemy.GetComponent<NavMeshAgent>().ResetPath();
        enemy.GetComponent<Follow>().enabled = true;
        obj.SetActive(false);
    }
}