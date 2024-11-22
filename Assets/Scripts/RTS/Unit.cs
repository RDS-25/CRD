using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelectable
{
    [SerializeField] UnitData unitData;
    [SerializeField] GameObject selectIndicator;

    bool isSelected = false;

    ICommand currentCommand;
    public float attackRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = unitData.moveSpeed;
        attackRange = unitData.attackRange;
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
            // 안되는거겠죠...
        }

    }

}
