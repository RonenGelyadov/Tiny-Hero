using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public void RandomPopUp() {
        transform.position = transform.position + new Vector3(Random.Range(-2f,2f), -0.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()){
            Destroy(gameObject);
        }
    }
}
