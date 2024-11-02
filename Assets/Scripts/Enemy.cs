using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Controle do Movimento")] 
    private Rigidbody2D rig;
    private Animator anim;
    private ArmControl armControl;

    [Header("Status")]
    [SerializeField] private float maxHealth = 100f; // Vida máxima do inimigo
    private float currentHealth; // Vida atual do inimigo
    private bool isDead = false; // Verifica se o inimigo está morto

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        currentHealth = maxHealth;
    }

    // Método para receber dano
    public void TakeDamage(float damage)
    {
        if (isDead) return; // Se já estiver morto, não recebe mais dano

        currentHealth -= damage;
        
        // Opcional: Tocar animação de dano
        // anim.SetTrigger("hurt");
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    // Método quando o inimigo morre
    private void Die()
    {
        if (isDead) return; // Evita que o método seja chamado múltiplas vezes
        
        isDead = true;
        
        // Opcional: Tocar animação de morte
        // anim.SetTrigger("die");
        
        // Opcional: Desabilitar componentes
        if(rig != null) rig.isKinematic = true;
        
        // Opcional: Desativar colisores
        Collider2D col = GetComponent<Collider2D>();
        if(col != null) col.enabled = false;
        
        // Opcional: Adicionar pontuação ou drops
        
        // Destruir o inimigo após um delay (opcional)
        Destroy(gameObject, 2f); // 2 segundos de delay
    }

    // Método para verificar se está morto (pode ser útil para outros scripts)
    public bool IsDead()
    {
        return isDead;
    }

    // Método para obter a vida atual (pode ser útil para UI)
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}