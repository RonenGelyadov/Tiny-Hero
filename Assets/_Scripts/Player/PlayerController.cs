using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private PlayerControls playerControls;
    private float moveDir;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private Knockback knockback;
    private bool isGrounded;

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
        groundCheck = transform.GetChild(0);
        isGrounded = false;
        EconomyManager.Instance.StartCoinsCounter();
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
        //moveDir = playerControls.Movement.Move.ReadValue<Vector2>().x;

        //animator.SetFloat("MoveX", moveDir);
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

    public void Jump() {
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

        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (hit) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }

    #region UI_Buttons

    public void UIMove(int moveDir) {
        this.moveDir = moveDir;
        animator.SetFloat("MoveX", this.moveDir);
    }

    #endregion
}
