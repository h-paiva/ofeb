using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float tempoParaDestruir = 0.5f; // Usado apenas se explodir no player
    public float damage = 1f; // Dano configurável da bala no Inspector
    private Animator animator;
    private bool explodiu = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (explodiu) return;

        if (colisao.gameObject.CompareTag("Player"))
        {
            Player player = colisao.collider.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            
            Debug.Log("Jogador acertado!");
            ExplodirComDelay(); // Permite a animação rodar antes de destruir
        }
        else if (colisao.gameObject.CompareTag("Ground"))
        {
            ExplodirInstantaneo(); // Some imediatamente ao tocar o chão
        }
    }

    void ExplodirComDelay()
    {
        explodiu = true;

        if (animator != null)
        {
            animator.SetTrigger("Explodir");
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;

        Destroy(gameObject, tempoParaDestruir);
    }

    void ExplodirInstantaneo()
    {
        explodiu = true;

        if (animator != null)
        {
            animator.SetTrigger("Explodir");
        }

        Destroy(gameObject); // Destrói imediatamente (sem delay)
    }
}
