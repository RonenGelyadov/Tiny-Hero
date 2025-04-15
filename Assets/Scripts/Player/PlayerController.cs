using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 3f;
    private PlayerControls playerControls;
    private float moveDir;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;

    public bool IsGrounded { get; private set; }
    
    protected override void Awake() {
        base.Awake();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Start() {
        playerControls.Movement.Jump.performed += _ => Jump();
        IsGrounded = false;
    }

    private void Update() {
        PlayerInput();
        ChangeFaceDirection();
        HandleAnimations();
    }

    private void FixedUpdate() {
        Move();
    }

    void PlayerInput() {
        moveDir = playerControls.Movement.Move.ReadValue<Vector2>().x;

        animator.SetFloat("MoveX", moveDir);
    }

    private void Move() {
        rb.linearVelocityX = moveDir * moveSpeed;
    }

    private void ChangeFaceDirection() {
        if (moveDir < 0) {
            spriteRenderer.flipX = true;
        } else if (moveDir > 0) {
            spriteRenderer.flipX = false;
        }
    }

    private void Jump() {
        if (IsGrounded) {
            IsGrounded = false;
            Vector2 jumpDir = Vector2.up * jumpForce;
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
        }
    }

    public void SetIsGrounded(bool isGrounded) {
        this.IsGrounded = isGrounded;
    }

    private void HandleAnimations() {
        animator.SetBool("IsGrounded", IsGrounded);
        animator.SetFloat("MoveY", rb.linearVelocityY);
    }
}
