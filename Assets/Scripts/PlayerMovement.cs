using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float smoothTime;
    private float currentVelocity;

    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;


    private CharacterController controller;
    private Animator anim;
    public Transform cameraTarget;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");
       
        //if (moveZ != 0 || moveX != 0)
        //{
            //this.transform.rotation = cameraTarget.transform.rotation;

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        //moveDirection = transform.TransformDirection(moveDirection);
        //Debug.Log(moveDirection);
        //}


        if (moveZ != 0 || moveX != 0)
        {
            Rotate();
            
        }



        if (isGrounded)
        {

            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if(moveDirection == Vector3.zero)
            {
                Idle();
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            anim.SetFloat("Speed", 0.05f, 0.1f, Time.deltaTime);

        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        //this.transform.LookAt(this.transform.position + moveDirection);
        //float singleStep = turnSpeed * Time.deltaTime;

        //Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, this.transform.position + moveDirection, singleStep, 0f);
        //Debug.DrawRay(transform.position, transform.position + moveDirection, Color.red);
    }

    private void Idle()
    {
        moveSpeed = 0;
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.3f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        anim.ResetTrigger("jumpAnim");
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetTrigger("jumpAnim");
    }

}
