using System.Collections;
using UnityEngine;

public class GoldCoin : MonoBehaviour {

    [SerializeField] private float travelHeight = 2f;
    [SerializeField] private float travelTime = 0.5f;
    [SerializeField] private AnimationCurve animCurve;

    public IEnumerator RandomPopUp() {

        Vector2 startPoint = transform.position;
        Vector2 randomPos = new Vector2(Random.Range(-2f, 2f), 0);
        Vector2 endPoint = startPoint + randomPos;

        float newTime = 0f;

        while (newTime < travelTime) {

            newTime += Time.deltaTime;
            float linearT = newTime / travelTime;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, travelHeight, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            Destroy(gameObject);
        }
    }
}
