using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
     public float tempoParaDestruir = 0.5f; // Tempo após explosão
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
            Debug.Log("Jogador acertado!");
            Explodir();
        }
        else if (colisao.gameObject.CompareTag("Chao"))
        {
            Explodir();
        }
    }

    void Explodir()
    {
        explodiu = true;
        animator.SetTrigger("Explodir");

        // Desativa a colisão para não explodir várias vezes
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;

        // Destroi após a animação
        Destroy(gameObject, tempoParaDestruir);
    }
}
