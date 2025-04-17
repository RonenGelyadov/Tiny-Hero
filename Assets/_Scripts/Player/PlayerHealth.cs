using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
	[SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }
}