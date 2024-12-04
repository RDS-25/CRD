using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class WispCotnroll : MonoBehaviour
{
    public List<GameObject> CommonUnit;
    UnitSelector unitSelector;
    NavMeshAgent agent;

    void Start()
    {
        unitSelector = FindObjectOfType<UnitSelector>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Swpan")
        {
            int rand = Random.Range(0, CommonUnit.Count);
            var a =ObjectPool.Instance.GetObjectUnit(CommonUnit[rand]);
            ObjectPool.Instance.ReturnObject(gameObject);
            unitSelector.DeselectAll();
        }
       
    }

 
}
