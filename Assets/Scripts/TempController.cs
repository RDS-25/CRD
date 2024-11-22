using UnityEngine;

public class TempController : MonoBehaviour
{
    CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        characterController.Move(new Vector3(moveX,0, moveZ) * 3 * Time.deltaTime);
        
    }
}
