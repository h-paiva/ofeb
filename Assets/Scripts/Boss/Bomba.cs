using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float tempoParaDestruir = 0.5f; // Usado apenas se explodir no player
    public float damage = 1f; // Dano configurável da bala no Inspector
    private Animator animator;
    private bool explodiu = false;

    private AudioSource audioSource;
    public AudioClip explosionSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.clip = explosionSound;

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
            Explodir(); // Permite a animação rodar antes de destruir
        }
        else if (colisao.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Chão");
            Explodir(); // Some imediatamente ao tocar o chão
        }
    }

    void Explodir()
    {       
            animator.SetTrigger("Explodir");
            audioSource.Play();
        

        //GetComponent<Collider2D>().enabled = false;
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //GetComponent<Rigidbody2D>().isKinematic = true;
        
        Destroy(gameObject, tempoParaDestruir);
    }
}
