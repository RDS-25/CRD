using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    CommandUI commandUI;
    UnitSelector unitSelector;

    private enum CommandMode 
    { 
        None, 
        Attack,
        Move
    }

    private CommandMode commandMode = CommandMode.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        commandUI = FindFirstObjectByType<CommandUI>();
        unitSelector = FindFirstObjectByType<UnitSelector>();
        // Find�� ���� ����� �ϴ� ����?
        // 1. Find�� ���� ���� �����ϴ� ��� ������Ʈ�� �˻���
        // �� ������ ����
        // 2. Find�� ���� ������Ʈ�� "�̸�"�� ��Ʈ�� ���ؼ� ã��
        // �� ������ - ��ҹ��� ��Ÿ�� ����??
        // �׷��ϱ� �������� ������.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            commandMode = CommandMode.Attack;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            commandMode = CommandMode.Move;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            HandleProduceCommand();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelCommandMode();
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            HandleLeftClick();
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            HandleRightClick();
        }
    }

    void CancelCommandMode()
    {
        if (commandMode != CommandMode.None)
        {
            commandMode = CommandMode.None;
        }
        else
        {
            unitSelector.DeselectAll();
        }

        commandUI.DisplayCommands(unitSelector.GetSelectedUnits());

    }

    void HandleProduceCommand()
    {
        List<ISelectable> selectedUnits = unitSelector.GetSelectedUnits();

        foreach (var selectable in selectedUnits)
        {
            if (selectable is Building building )
            {
                // ���� �����ϴ� ������ �ϳ����̹Ƿ� �ϴ� �̷����Ѵ�.
                // ���� ���� ������ ��������� GetComponents�� �����ͼ� ���Ͽ� Ŀ�ǵ带 ��������
                ICommand produceCommand = building.GetComponent<ProduceUnitCommand>();
                if (produceCommand!= null)
                {
                    building.ExecuteCommand(produceCommand);
                }
            }
        }
    }

    void HandleLeftClick()
    {
        if (commandMode == CommandMode.Attack)
        {
            HandleAttackCommand();
        }
        else if (commandMode== CommandMode.Move)
        {
            HandleMoveCommand();
        }
        else
        {
            HandleSelection();
        }
    }

    void HandleAttackCommand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit))
        {
            ISelectable target = hit.collider.GetComponent<ISelectable>();
            if (target != null)
            {
                List<ISelectable> selectedUnits = unitSelector.GetSelectedUnits();

                foreach (ISelectable selectable in selectedUnits)
                {
                    if (selectable is Unit unit) 
                    {
                        AttackCommand cmd = unit.GetComponent<AttackCommand>();
                        if (cmd!=null)
                        {
                            cmd.SetTarget(target);
                            unit.ExecuteCommand(cmd);
                        }
                    }
                }
                commandMode = CommandMode.None;
            }
            else
            {
                // ���ö�
            }
        }

    }

    void HandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            ISelectable selectable = hit.collider.GetComponent<ISelectable>();
            if (selectable != null)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    unitSelector.Select(selectable);
                }
                else 
                {
                    unitSelector.DeselectAll();
                    unitSelector.Select(selectable);
                }
            }
            else
            {
                unitSelector.DeselectAll();
            }
        }

        // commandUI.DisplayCommands(unitSelector.GetSelectedUnits());

    }

    void HandleRightClick()
    {
        if (commandMode == CommandMode.None)
        {
            HandleMoveCommand();
        }
    }

    void HandleMoveCommand()
    {
        Vector3 destination = GetMouseClickPosition();
        List<ISelectable> selectedUnit = unitSelector.GetSelectedUnits();

        foreach (ISelectable selectable in selectedUnit)
        {
            if (selectable is Unit unit)
            {
                MoveCommand moveCommand = unit.GetComponent<MoveCommand>();
                if (moveCommand != null)
                {
                    moveCommand.SetDestination(destination);
                    unit.ExecuteCommand(moveCommand);
                }
            }
        }

        commandMode = CommandMode.None;
    }

    Vector3 GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

// ���߿� ����� ��ġ�� �ʹٸ�, �׸��� ���� �ɸ��� �ɸ��ٸ�
// ���ٴ� ���̾ �и��ؼ� �� ���̾�� ���� ��� �˴ϴ�
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
