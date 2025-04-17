using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isOpened;

    private Animator animator;
    readonly int OPEN_HASH = Animator.StringToHash("Open");

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        isOpened = false;
    }

	private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>() && !isOpened) {
            isOpened = true;
            animator.SetTrigger(OPEN_HASH);
        }
    }
}
