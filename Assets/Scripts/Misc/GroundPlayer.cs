using UnityEngine;

public class GroundPlayer : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            PlayerController.Instance.SetIsGrounded(true);
        }
    }
}
