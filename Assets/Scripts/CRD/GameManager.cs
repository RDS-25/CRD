using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Transform cam;
    public float edgeThreshold = 10f; // 가장자리 감지 범위
    public float panSpeed = 5f;      // 카메라 이동 속도
    
    public Vector2 xLimits = new Vector3(-10f, 10f, 0f); // X축 이동 제한
    public Vector2 zLimits = new Vector3(-75f, -10f, 0f); // X축 이동 제한

    [SerializeField] private float swpantime = 1f;
    
    


   

    // Update is called once per frame
    void Update()
    {
        
        HandleMovement();
        
    }

    void HandleMovement()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 direction = Vector3.zero;

        if (mousePos.x <= edgeThreshold)
        {
            direction += Vector3.left;
        }
        else if (mousePos.x >= Screen.width - edgeThreshold)
        {
            direction += Vector3.right;
        }

        if (mousePos.y <= edgeThreshold)
        {
            direction += Vector3.back;
        }
        else if (mousePos.y >= Screen.height - edgeThreshold)
        {
            direction += Vector3.forward;
        }

        // 카메라 이동
        Vector3 newPosition = cam.transform.position + direction * panSpeed * Time.deltaTime;
        
        newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
        
        newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x,zLimits.y);
        
        cam.transform.position = newPosition;
        
    }
    // 포인터가 UI에 있을 때만 true 반환

}
