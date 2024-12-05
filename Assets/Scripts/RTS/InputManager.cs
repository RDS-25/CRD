using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    CommandUI commandUI;
    UnitSelector unitSelector;
    
    [Header("드래그")]
    [SerializeField]
    private	RectTransform		dragRectangle;			// 마우스로 드래그한 범위를 가시화하는 Image UI의 RectTransform

    private	Rect				dragRect;				// 마우스로 드래그 한 범위 (xMin~xMax, yMin~yMax)
    private	Vector2				start = Vector2.zero;	// 드래그 시작 위치
    private	Vector2				end = Vector2.zero;		// 드래그 종료 위치
	
    private	Camera				mainCamera;

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
        mainCamera			= Camera.main;
		
		
        // start, end가 (0, 0)인 상태로 이미지의 크기를 (0, 0)으로 설정해 화면에 보이지 않도록 함
        DrawDragRectangle();
 
    }
    private void DrawDragRectangle()
    {
        // 드래그 범위를 나타내는 Image UI의 위치
        dragRectangle.position	= (start + end) * 0.5f;
        // 드래그 범위를 나타내는 Image UI의 크기
        dragRectangle.sizeDelta	= new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
    }
    private void CalculateDragRect()
    {
        if ( Input.mousePosition.x < start.x )
        {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else
        {
            dragRect.xMin = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }

        if ( Input.mousePosition.y < start.y )
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }
    private void SelectUnits()
    {
        // 모든 유닛을 검사
        foreach ( Unit unit in ObjectPool.Instance.ShowUnitCount())
        {
            // 유닛의 월드 좌표를 화면 좌표로 변환해 드래그 범위 내에 있는지 검사
            if ( dragRect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)) )
            {
                ISelectable selectable = unit.GetComponent<ISelectable>();
                unitSelector.Select(selectable);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DragSelection();
        
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
            if (IsPointerOverUI())
            {
                return;
            }
            HandleLeftClick();
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            
            if (IsPointerOverUI())
            {
                return;
            }
            HandleRightClick();
        }
        
        commandUI.DisplayCommands(unitSelector.GetSelectedUnits());
        
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

        // commandUI.DisplayCommands(unitSelector.GetSelectedUnits());

    }

    void HandleProduceCommand()
    {
        List<ISelectable> selectedUnits = unitSelector.GetSelectedUnits();

        foreach (var selectable in selectedUnits)
        {
            if (selectable is Building building )
            {
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
            
            if (hit.collider.tag == "Player")
            {
                CancelCommandMode();
                return;
            }
            List<ISelectable> selectedUnits = unitSelector.GetSelectedUnits();
            if (target != null)
            {
                foreach (ISelectable selectable in selectedUnits)
                {
                    if (selectable is Unit unit) 
                    {
                        unit.forcedattack = true;
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
                foreach (ISelectable selectable in selectedUnits)
                {
                    if (selectable is Unit unit) 
                    {
                        unit.forcedattack = false;
                    }
                }
                commandMode = CommandMode.None;
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
                    // 이미 선택되어 있다면 해제, 아니면 선택
                    if (unitSelector.IsSelected(selectable))
                    {
                        unitSelector.Deselect(selectable);
                    }
                    else
                    {
                        unitSelector.Select(selectable);
                    }
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
    }

    void DragSelection()
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            start	 = Input.mousePosition;
            dragRect = new Rect();
        }
		
        if ( Input.GetMouseButton(0) )
        {
            end = Input.mousePosition;
			
            // 마우스를 클릭한 상태로 드래그 하는 동안 드래그 범위를 이미지로 표현
            DrawDragRectangle();
        }

        if ( Input.GetMouseButtonUp(0) )
        {
            // 마우스 클릭을 종료할 때 드래그 범위 내에 있는 유닛 선택
            CalculateDragRect();
            SelectUnits();

            // 마우스 클릭을 종료할 때 드래그 범위가 보이지 않도록
            // start, end 위치를 (0, 0)으로 설정하고 드래그 범위를 그린다
            start = end = Vector2.zero;
            DrawDragRectangle();
        }
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
                unit.forcedattack = false;
                MoveCommand moveCommand = unit.GetComponent<MoveCommand>();
                if (moveCommand != null)
                {
                    moveCommand.SetDestination(destination);
                    unit.ExecuteCommand(moveCommand);
                }
            }
        }

        // commandMode = CommandMode.None;
    }

    Vector3 GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    
    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
