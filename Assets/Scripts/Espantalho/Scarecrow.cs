using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scarecrow : MonoBehaviour
{
    [Header("Vida do Espantalho")]
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;

    [Header("UI de Vida")]
    [SerializeField] private Slider lifeBar;

    [Header("Feedback Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        if (lifeBar != null)
        {
            lifeBar.maxValue = maxHealth;
            lifeBar.value = maxHealth;
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (lifeBar != null)
            lifeBar.value = currentHealth;

        if (spriteRenderer != null)
            StartCoroutine(HitFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Espantalho destruído!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 2f, 0f));
    }
}