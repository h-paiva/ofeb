using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Controle do Movimento")] 
    private Rigidbody2D rig;
    private Animator anim;
    private ArmControl armControl;
    public EnemyLifeBar enemyLifeBar;
    public GameObject ArmEnemy;

    [Header("Status")]
    [SerializeField] private float maxHealth = 100f; // Vida máxima do inimigo
    private float currentHealth; // Vida atual do inimigo
    private bool isDead = false; // Verifica se o inimigo está morto

    [Header("FSM Configurações")]
    private Transform playerTransform;
    public float talkRange = 10f; // Distância para falar
    public float attackRange = 5f; // Distância para atacar
    public float distanceToPlayer; // Torna a distância ao jogador pública

    private enum State { Idle, Talk, Attack }
    private State currentState = State.Idle;

    private float dialogueTimer;

    [Header("Diálogo")]
    public Canvas dialogueCanvas; // Canvas para exibir falas
    public Text dialogueText; // Texto dentro do Canvas
    public float dialogueDuration = 2f; // Duração de cada fala
    [TextArea(2, 5)] public List<string> randomPhrases; // Lista de frases aleatórias

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        currentHealth = maxHealth;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (dialogueCanvas != null)
        {
            dialogueCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isDead) return;

        // Calcula e atualiza a distância ao jogador
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Lógica da Máquina de Estados Finitos
        switch (currentState)
        {
            case State.Idle:
                HandleIdleState(distanceToPlayer);
                break;
            case State.Talk:
                HandleTalkState(distanceToPlayer);
                break;
            case State.Attack:
                HandleAttackState(distanceToPlayer);
                break;
        }
    }

    private void HandleIdleState(float distanceToPlayer)
    {
        if (distanceToPlayer <= talkRange)
        {
            currentState = State.Talk;
            ShowRandomDialogue();
        }
    }

    private void HandleTalkState(float distanceToPlayer)
    {
        if (distanceToPlayer > talkRange)
        {
            currentState = State.Idle;
            HideDialogue();
        }
        else if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attack;
            HideDialogue();
        }
        else
        {
            dialogueTimer -= Time.deltaTime;
            if (dialogueTimer <= 0)
            {
                ShowRandomDialogue();
            }
        }
    }

    private void HandleAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            currentState = State.Talk;
            ShowRandomDialogue();
        }
        else
        {
            anim.SetBool("isAttacking", true);
            // Lógica de ataque aqui
        }
    }

    private void ShowRandomDialogue()
    {
        if (dialogueCanvas != null && randomPhrases.Count > 0)
        {
            string randomPhrase = randomPhrases[Random.Range(0, randomPhrases.Count)];
            dialogueText.text = randomPhrase;
            dialogueCanvas.gameObject.SetActive(true);
            dialogueTimer = dialogueDuration;
        }
    }

    private void HideDialogue()
    {
        if (dialogueCanvas != null)
        {
            dialogueCanvas.gameObject.SetActive(false);
        }
    }

    // Método para receber dano
    public void TakeDamage(float damage)
    {
        enemyLifeBar.DamageLife(damage);
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
        
        
        
        
        
        // Opcional: Adicionar pontuação ou drops
        Destroy(ArmEnemy, 0f);
        anim.SetBool("death", true);
        
        // Destruir o inimigo após um delay (opcional)
        Destroy(gameObject, 1f); // 2 segundos de delay
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // Cor para a área de falar
        Gizmos.DrawWireSphere(transform.position, talkRange);

        Gizmos.color = Color.red; // Cor para a área de ataque
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}