using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private LayerMask groundLayer;

    private PlayerControls playerControls;
    private float moveDir;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private Knockback knockback;

    private float rayLength = 0.6f;
    bool isGrounded;

    const string GROUND_LAYER_TEXT = "Ground";

    protected override void Awake() {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        knockback = GetComponent<Knockback>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Start() {
        playerControls.Movement.Jump.performed += _ => Jump();
        isGrounded = false;
    }

    private void Update() {

        HandleAnimations();
        CheckIfGrounded();

        if (knockback.GetKnockback) { return; }

        PlayerInput();
        ChangeFaceDirection();
    }

    private void FixedUpdate() {

        if (knockback.GetKnockback) { return; }

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
        }
        else if (moveDir > 0) {
            spriteRenderer.flipX = false;
        }
    }

    private void Jump() {
        if (isGrounded) {
            isGrounded = false;
            Vector2 jumpDir = Vector2.up * jumpForce;
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
        }
    }

    private void HandleAnimations() {
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("MoveY", rb.linearVelocityY);
    }

    private void CheckIfGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        if (hit) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(GROUND_LAYER_TEXT)) {
                isGrounded = true;
            }
            else {
                isGrounded = false;
            }
        }
        else {
            isGrounded = false;
        }
    }
}
