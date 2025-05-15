using System.Collections;
using UnityEngine;

public class GoldCoin : MonoBehaviour {

    [SerializeField] private float travelHeight = 2f;
    [SerializeField] private float travelTime = 0.2f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private GameObject coinDestroyVFX;

    private readonly float finalHeight = -0.5f;

    private bool isFinishedTraveling;

    public IEnumerator RandomPopUp() {

        isFinishedTraveling = false;

        Vector2 startPoint = transform.position;
        Vector2 randomPos = new Vector2(Random.Range(-3f, 3f), finalHeight);
        Vector2 endPoint = startPoint + randomPos;

        float newTime = 0f;

        while (newTime < travelTime) {

            newTime += Time.deltaTime;
            float linearT = newTime / travelTime;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, travelHeight, heightT);

            Vector2 vector2 = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            transform.position = vector2;

            yield return null;
        }

        isFinishedTraveling = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>() && isFinishedTraveling) {
            EconomyManager.Instance.UpdateCoins();
            Instantiate(coinDestroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
