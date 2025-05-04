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

    [Header("Diálogo")]
    private Transform playerTransform;
    public float talkRange = 10f; // Distância para falar
    public float distanceToPlayer; // Torna a distância ao jogador pública

    public Canvas dialogueCanvas; // Canvas para exibir falas
    public Text dialogueText; // Texto dentro do Canvas
    public float dialogueDuration = 10f; // Duração de cada fala
    [TextArea(2, 5)] public List<string> randomPhrases; // Lista de frases aleatórias

    // Adicionado: tempo restante para nova fala
    private float dialogueTimer;

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

        // Atualiza distância ao jogador
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= talkRange)
        {
            dialogueTimer -= Time.deltaTime;
            if (!dialogueCanvas.gameObject.activeSelf || dialogueTimer <= 0f)
            {
                ShowRandomDialogue();
            }
        }
        else
        {
            HideDialogue();
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

            dialogueCanvas.gameObject.SetActive(false);
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
}
