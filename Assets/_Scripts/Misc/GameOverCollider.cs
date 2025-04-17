using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCollider : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            Destroy(PlayerController.Instance.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
