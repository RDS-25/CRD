using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelectable
{
 
    public UnitData unitData;
    [SerializeField] GameObject selectIndicator;

    bool isSelected = false;

    ICommand currentCommand;
    public float attackRange;
    public float hp;
    public float mp;
  
   
  
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = unitData.moveSpeed;
        attackRange = unitData.attackRange;
        hp = unitData.health;
        mp = unitData.magicPoint;
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
            //행동 안될때 
        }

    }

}
