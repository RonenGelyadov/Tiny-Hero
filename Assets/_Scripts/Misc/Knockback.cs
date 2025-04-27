using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour {

    [SerializeField] private float knockbackThrust = 5f;
    private float knockbackDelay = 1f;

    private Rigidbody2D rb;

    public bool GetKnockback { get; private set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        GetKnockback = false;
    }

    public void KnockBack(Vector3 target) {

        GetKnockback = true;

        rb.linearVelocity = Vector3.zero;

        Vector2 knockDir = (target - transform.position).normalized;

        if (knockDir.x > 0) {
            rb.AddForce(new Vector2(-knockbackThrust, knockbackThrust), ForceMode2D.Impulse);
        }
        else {
            rb.AddForce(new Vector2(knockbackThrust, knockbackThrust), ForceMode2D.Impulse);
        }

        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine() {
        yield return new WaitForSeconds(knockbackDelay);
        GetKnockback = false;
    }
}
