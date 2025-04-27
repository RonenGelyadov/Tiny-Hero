using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour {

    [SerializeField] private Material whiteMat;
    [SerializeField] private float defaultMatRestoreTime = 0.1f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine() {
        spriteRenderer.material = whiteMat;
        yield return new WaitForSeconds(defaultMatRestoreTime);
        spriteRenderer.material = defaultMat;
    }
}
