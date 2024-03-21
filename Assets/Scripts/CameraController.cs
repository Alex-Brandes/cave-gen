using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    public Transform camTarget;
    public Transform player;
    private PlayerMovement playerScript;
    //private Transform parent;

    private void Start()
    {
        //parent = transform.parent;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        camTarget.position = player.position;
        if (Input.GetKeyDown(KeyCode.Mouse1) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            rotateCamera();
        }
    }

    
    private void rotateCamera()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        camTarget.transform.Rotate(Vector3.up, mouseX);
    }
}
