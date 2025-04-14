using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 3f;
    private PlayerControls playerControls;
    private Vector3 moveDir;
    private Rigidbody2D rb;
    private Animator animator;
    
    protected override void Awake() {
        base.Awake();
        
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
        
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();
    }

    void PlayerInput() {
        moveDir = playerControls.Movement.Move.ReadValue<Vector2>();

        
    }

    private void Move() {
        rb.MovePosition(transform.position + (moveDir * moveSpeed * Time.fixedDeltaTime));
    }
}
