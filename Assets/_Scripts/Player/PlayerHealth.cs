using System.Collections;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth> {
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageCoolDown = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        if (canTakeDamage) {

            canTakeDamage = false;
            currentHealth -= damage;
            Debug.Log("Health: " + currentHealth);
            StartCoroutine(TakingDamageRoutine());
        }
    }

    private IEnumerator TakingDamageRoutine() {
        yield return new WaitForSeconds(damageCoolDown);
        canTakeDamage = true;
    }
}