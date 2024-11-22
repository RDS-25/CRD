using UnityEngine;

public class ProduceUnitCommand : MonoBehaviour, ICommand 
{
    [SerializeField] UnitData unitData;

    public string Name => "Produce" + unitData.unitName;

    public void Cancel()
    {
    }

    public bool CanExecute(ISelectable executor)
    {
        return true;
    }

    public void Execute(ISelectable executor)
    {
        if (executor is Building building)
        {
            building.StartUnitProduction(unitData);
        }
    }

}
