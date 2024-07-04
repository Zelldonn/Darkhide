using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class FPSController : MonoBehaviour
{
    public float senisitivity = 100f;
    public float walkSpeed = 2.4f;
    public float runSpeed = 5.5f;
    public float speedSmoothTime = 0.2f;
    public float gravity = -9f;
    public float jumpHeight = 1f;

    public float coyoteTime = 2f;
    private float lastTimeGrounded;
    private bool isJumping = false;


    float speedSmoothVelocity;

    float currentSpeed;
    Vector3 targetAngles;
    float velocityY;

    CharacterController controller;
    Transform cameraTransform;
    Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        GetComponentInChildren<Camera>().enabled = true;
        animator = GetComponent<Animator>();
        targetAngles = new Vector3(0,0,0);
        cameraTransform.localEulerAngles = targetAngles;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponentInParent<Transform>().position = new Vector3(100f, 0f, 100f);
        
       
        Vector2 keyboardRawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 keyboardDir = keyboardRawInput;
        keyboardDir.Normalize();

        if (controller.isGrounded && !isJumping)
        {
            lastTimeGrounded = Time.time;
        }
        if (controller.isGrounded)
            isJumping = false;

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }


        bool runningDiagLeft = keyboardRawInput == new Vector2(-1, 1);
        bool runningDiagRight = keyboardRawInput == new Vector2(1, 1);
        bool runningStraight = keyboardRawInput == new Vector2(0, 1);

        bool running = Input.GetKey(KeyCode.LeftShift) && (runningStraight || runningDiagLeft || runningDiagRight);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * keyboardDir.magnitude ;
        if (!controller.isGrounded)
        {
            targetSpeed *= 1f;
        }
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        Vector3 finalDirection = (keyboardDir.x * cameraTransform.right + keyboardDir.y * transform.forward) * currentSpeed + Vector3.up * velocityY;

        controller.Move(finalDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
            animator.SetBool("IsJumping", false);
        }

        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("SpeedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    private void LateUpdate()
    {
        Vector2 mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.eulerAngles += Vector3.up * mouseDir.x * senisitivity * Time.deltaTime;


        targetAngles.x = Mathf.Clamp(targetAngles.x - mouseDir.y * senisitivity * Time.deltaTime, -70, 80);
        cameraTransform.localEulerAngles = targetAngles;

    }

    void Jump()
    {
        if (lastTimeGrounded <= Time.time + coyoteTime)
        {
            isJumping = true;
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            animator.SetBool("IsJumping", true);
        }
    }

    public float getCurrentSpeed()
    {
        return currentSpeed;
    }
}
