using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public EnemyData enemyPrefabs; // 이 ScriptableObject로 적 프리팹을 관리합니다.
    [SerializeField]private List<GameObject> pool = new List<GameObject>();
    private Dictionary<int, GameObject> stagePrefabs;
    [SerializeField] private Transform swpanPos;
    [SerializeField] private Transform swpanGroups;

    private void Awake()
    {
        InitializeDictionary();
        InitializePool();
     
    }

   

    private void InitializeDictionary()
    {
        stagePrefabs = new Dictionary<int, GameObject>();

        for (int i = 0; i < enemyPrefabs.enemyProperties.Count; i++)
        {
            stagePrefabs.Add(i + 1, enemyPrefabs.enemyProperties[i].prefab); // 각 스테이지에 대한 프리팹 매핑
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