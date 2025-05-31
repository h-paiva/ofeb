using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardeiro : MonoBehaviour
{
    [Header("Controle do Movimento")] 
    public Transform pontoEsquerdo; // Limite esquerdo
    public Transform pontoDireito; // Limite direito
    private bool indoParaDireita = true;
    public float velocidade = 3f;

    [Header("Bombas")] 
    public GameObject bombaPrefab;
    public Transform pontoDeSoltarBomba;
    public float toleranciaParaSoltar = 0.5f;
    public float tempoEntreBombas = 2f;
    private float tempoDesdeUltimaBomba;
    public Transform player; // Jogador

    [Header("Status")] 
    [SerializeField] private float maxHealth = 100f; // Vida máxima do inimigo
    public float currentHealth; // Vida atual do inimigo
    private bool isDead = false; // Verifica se o inimigo está morto
    private bool activeBoss = false; // Ativa a mecanica do Boss
    public EnemyLifeBar enemyLifeBar;
    private Animator anim;

    public ShowPanel showPanel;

    void Start()
    {
        
        anim = GetComponent<Animator>();


        // Busca o jogador pela tag
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null)
        {
            player = jogador.transform;
        }
        
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (activeBoss)
        {

            if (isDead) return;

            if (player == null || pontoEsquerdo == null || pontoDireito == null)
                return;

            tempoDesdeUltimaBomba += Time.deltaTime;

            // Define direção com base no sentido atual
            float direcao = indoParaDireita ? 1 : -1;

            // Move o bombardeiro horizontalmente
            transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

            // Verifica limites
            if (transform.position.x >= pontoDireito.position.x)
                indoParaDireita = false;
            else if (transform.position.x <= pontoEsquerdo.position.x)
                indoParaDireita = true;

            // Define o scale para "virar" visualmente
            Vector3 escala = transform.localScale;
            escala.x = indoParaDireita ? Mathf.Abs(escala.x) : -Mathf.Abs(escala.x);
            transform.localScale = escala;

            // Verifica se está em cima do jogador
            if (Mathf.Abs(transform.position.x - player.position.x) < toleranciaParaSoltar &&
                tempoDesdeUltimaBomba >= tempoEntreBombas)
            {
                SoltarBomba();
                tempoDesdeUltimaBomba = 0f;
                Debug.Log("Soltou a bomba!");
            }
        }
    }

    void SoltarBomba()
    {
        Instantiate(bombaPrefab, pontoDeSoltarBomba.position, Quaternion.identity);
    }

    // Método para permitir que o jogador se mova mais quando estiver lutando contra o boss final da fase 2
    public void SetActiveBoss(bool finalBossActive)
    {
        activeBoss = finalBossActive;
        Debug.Log("ATIVOU O BOSS");
    }

    // Método para receber dano
    public void TakeDamage(float damage)
    {
        Debug.Log("TOMOU DANO");
        enemyLifeBar.DamageLife(damage);
        if (isDead) return; // Se já estiver morto, não recebe mais dano

        currentHealth -= damage;
        
        // Opcional: Tocar animação de dano
        // anim.SetTrigger("hurt");
        
        if(currentHealth <= 0)
        {
            Debug.Log("DESTRUIU O BOSS");
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
        anim.SetBool("death", true);

        // Destruir o inimigo após um delay (opcional)
        Destroy(gameObject, 0.3f); // 5 segundos de delay
        
        StartCoroutine(FreezeAfterDelay(0.5f));

    }

    private System.Collections.IEnumerator FreezeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // <--- ESSENCIAL

        Time.timeScale = 0f; // Pause game
        showPanel.ShowPanelAction(); // Show panel
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
