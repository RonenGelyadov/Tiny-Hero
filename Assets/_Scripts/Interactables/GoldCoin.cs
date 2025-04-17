using Unity.Mathematics;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()){
            Destroy(gameObject);
        }
    }
}
