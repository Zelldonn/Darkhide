using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float gravity = -9f;
    public float jumpHeight = 1f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.2f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    CharacterController controller;

    public bool isJumping = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed: walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity =  transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        //currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded){
            velocityY = 0;
            animator.SetBool("IsJumping", false);
        }
        
        float animationSpeedPercent = ((running) ? currentSpeed/runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("SpeedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            animator.SetBool("IsJumping", true);
        }
    }
}
