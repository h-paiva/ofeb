using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float health = 3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Caixa recebeu " + damage + " de dano. Vida restante: " + health);

        if (health <= 0)
        {
            Debug.Log("Caixa destruída!");
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 normal = collision.contacts[0].normal;
            bool playerEmCima = normal.y < -0.5f;
            bool playerDoLado = Mathf.Abs(normal.x) > 0.5f;

            if (playerEmCima)
            {
                Animator anim = collision.collider.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.Play("Idle");
                }

                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (rbPlayer != null && Mathf.Abs(rbPlayer.velocity.y) < 0.01f && Mathf.Abs(rbPlayer.velocity.x) < 0.1f)
                    {
                        Animator animPlayer = player.GetComponent<Animator>();
                        animPlayer.SetBool("walk", false);
                        animPlayer.SetBool("run", false);
                        animPlayer.SetBool("jump", false);
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

            Player playerFinal = collision.gameObject.GetComponent<Player>();
            if (playerFinal != null)
            {
                Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rbPlayer != null && Mathf.Abs(rbPlayer.velocity.y) < 0.01f && Mathf.Abs(rbPlayer.velocity.x) < 0.1f)
                {
                    Animator anim = playerFinal.GetComponent<Animator>();
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);
                    anim.SetBool("jump", false);
                }
            }
        }
    }
}
