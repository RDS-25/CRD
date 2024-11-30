using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    public EnemyData enemyPrefabs;
    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private List<GameObject> poolCharGameObjects = new List<GameObject>();
    private Dictionary<int, GameObject> stagePrefabs;
    [SerializeField] private Transform swpanPos;
    [Header("Initialized spawned enemies")]
    [SerializeField] private Transform swpanGroups;

    private void Awake()
    {
        // 싱글턴
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
        foreach (var property in enemyPrefabs.enemyProperties)
        {
            for (int i = 0; i < 35; i++)
            {
                GameObject obj = Instantiate(property.prefab);
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
    

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}