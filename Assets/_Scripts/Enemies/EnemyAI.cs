using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float patrolRange = 10f;
    
    private float offsetXLeft, offsetXRight;
    private float offsetYUp, offsetYDown;
    private Vector3 moveDir;

	private Rigidbody2D rb;
    
    private enum PatrolMode {
        Horizontal,
        Vertical
    }

    [SerializeField] PatrolMode currentPatrolMode = PatrolMode.Horizontal;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        GetPatrolingLimits();

        if (currentPatrolMode == PatrolMode.Horizontal) {
            moveDir = Vector3.left;
        } else if (currentPatrolMode == PatrolMode.Vertical) {
            moveDir = Vector3.down;
        }
    }

    private void FixedUpdate() {
        PatrolModeControl();
    }

    private void PatrolModeControl() {     

        switch (currentPatrolMode) {
            case PatrolMode.Horizontal: 
                Patrol(true);
                break;

            case PatrolMode.Vertical:
                Patrol(false);
                break;

            default:
                break;            
        }
    }

    private void Patrol(bool isHorizontal) {
        rb.MovePosition(transform.position + (moveDir * moveSpeed * Time.fixedDeltaTime));
        
        if (isHorizontal) {
            if (transform.position.x < offsetXLeft) {
                moveDir = Vector3.right;
            } else if (transform.position.x > offsetXRight) {
                moveDir = Vector3.left;
            }

        } else {
            if (transform.position.y < offsetYDown) {
                moveDir = Vector3.up;
            } else if (transform.position.y > offsetYUp) {
                moveDir = Vector3.down;
            }
        }
    }

    private void GetPatrolingLimits() {
        offsetXRight = transform.position.x + (patrolRange / 2);
        offsetXLeft = transform.position.x - (patrolRange / 2);

        offsetYUp = transform.position.y + (patrolRange / 2);
        offsetYDown = transform.position.y - (patrolRange / 2);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1);
    }
}
