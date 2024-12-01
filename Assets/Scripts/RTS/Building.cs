using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable 
{
    [SerializeField] BuildingData buildingData;
    [SerializeField] GameObject selectIndicator;
    [SerializeField] Transform spawnPoint;

    UnitData[] producibleUnits;
    Coroutine activeProductionCoroutine;

    bool isSelected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name = buildingData.name;
        producibleUnits = buildingData.producableUnits;
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
        if (command.CanExecute(this))
        {
            command.Execute(this);
        }
        else
        {
            // ��...
        }
    }

    public bool CanProduceUnit(UnitData unitData)
    {
        if (!buildingData.canProduceUnits) return false;

        foreach (var unit in producibleUnits)
        {
            if (unit==unitData) return true;
        }

        return false;
    }

    public void StartUnitProduction(UnitData unitData)
    {
        if (activeProductionCoroutine != null)
        {
            Debug.Log("already in progress");
            return;
        }

        activeProductionCoroutine = StartCoroutine(ProduceUnit(unitData));

    }

    IEnumerator ProduceUnit(UnitData unitData)
    {
        StateMachine stateMachine = GetComponent<StateController>().stateMachine;
        stateMachine.TransitionTo(stateMachine.productionState);

        yield return new WaitForSeconds(1f);

        // SpawnUnit(unitData);

        stateMachine.TransitionTo(stateMachine.idleState);

        activeProductionCoroutine = null;
    }

    // void SpawnUnit(UnitData unitData)
    // {
    //     //2024.11.26 잠시 제거
    //     // Instantiate(unitData.prefab, spawnPoint.position, Quaternion.identity);
    // }
}
