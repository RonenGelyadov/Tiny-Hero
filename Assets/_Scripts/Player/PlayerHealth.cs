using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealth : Singleton<PlayerHealth> {
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageCoolDown = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;

    private Flash flash;

    private const string HEALTH_AMOUNT_TEXT = "Health Amount Text";
    private TMP_Text healthAmountText;

    protected override void Awake() {
        base.Awake();
        flash = GetComponent<Flash>();
    }

    private void Start() {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage) {
        if (canTakeDamage) {

            canTakeDamage = false;
            currentHealth -= damage;
            UpdateHealthText();
            StartCoroutine(TakingDamageRoutine());
            StartCoroutine(flash.FlashRoutine());
        }
    }

    private IEnumerator TakingDamageRoutine() {
        yield return new WaitForSeconds(damageCoolDown);
        canTakeDamage = true;
    }

    private void UpdateHealthText() {

        if (healthAmountText == null) {
            healthAmountText = GameObject.Find(HEALTH_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        healthAmountText.text = currentHealth.ToString();
    }
}