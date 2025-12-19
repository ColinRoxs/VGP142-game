using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerController : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 lastPos;

    private float currentMoveSpeed;

    private int forwardHash;
    private int rightHash;

    private void Start()
    {
        currentMoveSpeed = baseMoveSpeed;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        forwardHash = Animator.StringToHash("Forward");
        rightHash = Animator.StringToHash("Right");
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        currentMoveSpeed = baseMoveSpeed * GameManager.Instance.collectableCount + baseMoveSpeed;

        // Get movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move relative to player's forward direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * currentMoveSpeed * Time.deltaTime);

        // Gravity
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 disp = transform.position - lastPos;
        Vector3 vel = disp / Mathf.Max(Time.deltaTime, 0.0001f);
        vel.y = 0f;

        Vector3 localVelocity = transform.InverseTransformDirection(vel);


        // --- Animation Control ---
        // If player is pressing movement keys, set Forward to 1, else 0
        float forwardAmount = Mathf.Clamp(localVelocity.z / currentMoveSpeed, -1f, 1f);
        float rightAmount = Mathf.Clamp(-localVelocity.x / currentMoveSpeed, -1f, 1f);

        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.SetFloat(forwardHash, forwardAmount);
            animator.SetFloat(rightHash, rightAmount);
        }

        lastPos = transform.position;
    }
}


