using UnityEngine;

public class PropertyDisplay : MonoBehaviour ,ISelectable
{
    
    bool isSelected = false;
    ICommand currentCommand;

    public float speed;
    public float ammor;
    public float maxhp;
    public float currenthp;

    void Start()
    {
        currenthp = maxhp;
    }

    public void Select()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }

    public bool IsSelected()
    {
        return isSelected;
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
