using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f; // Vida total da caixa (3 tiros de 10 de dano)
    [SerializeField] private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer da caixa
    [SerializeField] private Color damageColor = Color.gray; // Cor escurecida por dano
    [SerializeField] private GameObject breakEffect; // Efeito visual ao quebrar a caixa
    [SerializeField] private Rigidbody2D rb;

    private float currentHealth;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer não encontrado na caixa.");
        if (rb == null)
            Debug.LogError("Rigidbody2D não encontrado na caixa.");

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Caixa recebeu dano: " + damage); //Pra saber se está computando a bala
        currentHealth -= damage;
        UpdateBoxColor();

        if (currentHealth <= 0)
        {
            BreakBox();
        }
    }

    private void UpdateBoxColor()
    {
        // Escurece a cor proporcionalmente ao dano
        /*float healthPercent = Mathf.Clamp01(currentHealth / maxHealth);
        spriteRenderer.color = Color.Lerp(damageColor, originalColor, healthPercent);*/

        if (spriteRenderer != null)
        {
            float intensity = currentHealth / maxHealth; // de 1 (normal) até 0 (prestes a quebrar)
            spriteRenderer.color = new Color(intensity, intensity, intensity, 1f); // escurece com base na vida
        }
    }

    private void BreakBox()
    {
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        Debug.Log("Caixa quebrou!");
        Destroy(gameObject);
    }
}
