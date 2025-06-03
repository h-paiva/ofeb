using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float health = 30f; // Vida total da caixa (30 tiros de 1 de dano)
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer da caixa
    [SerializeField] private Color damageColor = Color.gray; // Cor escurecida por dano
    [SerializeField] private GameObject breakEffect; // Efeito visual ao quebrar a caixa

    [Header("Som da Quebra da Caixa")]
    public AudioClip clipDamageBox;

    public AudioSource audioSourceDamageBox;

    private float initialHealth; // Valor inicial da vida, usado para cálculo de cor
    private Color originalColor;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        //Instanciando o audio
        audioSourceDamageBox = GetComponent<AudioSource>();
        if (audioSourceDamageBox == null)
            audioSourceDamageBox = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        // Inicializa vida e cor
        initialHealth = health; // Guarda o valor inicial da vida
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        print("Acertou a caixa");

        StartCoroutine(FlashOnHit()); // Pisca em cinza

        UpdateBoxColor(); // Escurece a cor conforme dano

        if (health <= 0)
        {
            BreakBox(); // Efeito visual
            Destroy(gameObject);
        }
        
        //Toca o som ao ser atingido
        if (clipDamageBox != null && audioSourceDamageBox != null)
        {
            audioSourceDamageBox.PlayOneShot(clipDamageBox);
        }
    }

    // Escurece a caixa conforme a vida
    private void UpdateBoxColor()
    {
        if (spriteRenderer != null && initialHealth > 0)
        {
            float intensity = Mathf.Clamp01(health / initialHealth); // de 1 (normal) até 0 (prestes a quebrar)
            spriteRenderer.color = new Color(intensity, intensity, intensity, 1f); // escurece com base na vida
        }
    }

    // Instancia o efeito visual de quebra
    private void BreakBox()
    {
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionStay2D(Collision2D collisionPlayer)
    {
        if (collisionPlayer.collider.CompareTag("Player"))
        {
            Vector2 normal = collisionPlayer.contacts[0].normal;
            bool playerEmCima = normal.y < -0.5f;
            bool playerDoLado = Mathf.Abs(normal.x) > 0.5f;

            if (playerEmCima)
            {
                Animator anim = collisionPlayer.collider.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.Play("Idle");
                }

                Player player = collisionPlayer.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    Rigidbody2D rbPlayer = collisionPlayer.gameObject.GetComponent<Rigidbody2D>();
                    if (rbPlayer != null && Mathf.Abs(rbPlayer.velocity.y) < 0.01f && Mathf.Abs(rbPlayer.velocity.x) < 0.1f)
                    {
                        Animator animPlayer = player.GetComponent<Animator>();
                        animPlayer.SetBool("walk", false);
                        animPlayer.SetBool("run", false);
                        //animPlayer.SetBool("jump", false);
                    }
                }

                rb.velocity = new Vector2(0f, rb.velocity.y);
                return;
            }

            if (playerDoLado)
            {
                float moveInput = Input.GetAxis("Horizontal");
                if (Mathf.Abs(moveInput) > 0.1f)
                {
                    Vector3 newVelocity = new Vector3(moveInput * 2f, rb.velocity.y, 0f);
                    rb.velocity = new Vector2(newVelocity.x, newVelocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
            }

            Player playerFinal = collisionPlayer.gameObject.GetComponent<Player>();
            if (playerFinal != null)
            {
                Rigidbody2D rbPlayer = collisionPlayer.gameObject.GetComponent<Rigidbody2D>();
                if (rbPlayer != null && Mathf.Abs(rbPlayer.velocity.y) < 0.01f && Mathf.Abs(rbPlayer.velocity.x) < 0.1f)
                {
                    Animator anim = playerFinal.GetComponent<Animator>();
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);
                    //anim.SetBool("jump", false);
                }
            }
        }
    }

    // Método para obter a vida atual da caixa (pode ser útil para UI futuramente)
    public float GetCurrentHealth()
    {
        return health;
    }

    // Pisca a caixa com cor de dano e retorna à cor original
    private IEnumerator FlashOnHit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }
}
