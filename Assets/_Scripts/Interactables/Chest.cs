using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int minDropAmount = 1;
    [SerializeField] private int maxDropAmount = 7;
    [SerializeField] private float timeBetweenDrops = 0.8f;

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

    private IEnumerator EmptyingChestAnimEvent() {  
        int dropAmount = Random.Range(minDropAmount, maxDropAmount);

        if (dropAmount > 4) {
            timeBetweenDrops = 0.1f;
        } else {
            timeBetweenDrops = 2f;
        }
        
        for (int i = 0; i < dropAmount; i++) {
            GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            newCoin.GetComponent<GoldCoin>().RandomPopUp();
            yield return new WaitForSeconds(timeBetweenDrops);
        }   
    }
}
