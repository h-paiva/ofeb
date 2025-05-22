using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar nova cena

public class CheckpointFinal : MonoBehaviour
{
    [Header("Mensagem do Capitão")]
    [TextArea]
    [SerializeField] private string mensagemFinal = "Parabens ''Axpira'', nao foi tao ruim quanto pensei, mas ainda precisamos vencer as nossas missões. Vamos que irei te orientar sobre elas.";

    private bool jaFinalizou = false; // Garante que o checkpoint só será ativado uma vez

    // Este método é chamado quando outro collider entra na área do trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se já finalizou ou não for o jogador, não faz nada
        if (jaFinalizou || !collision.CompareTag("Player")) return;

        // Para o contador do tutorial e congela o jogador
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.PararContagem();
            jaFinalizou = true;

            // Congela o jogador para evitar movimentação
            Player.isFrozen = true;

            // Zera os parâmetros da animação para evitar que fique preso na pose de andar ou correr
            Animator anim = collision.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                anim.SetBool("jump", false);
            }

            // Aguarda 3 segundos e carrega a próxima cena
            Invoke("CarregarProximaCena", 3f);
        }

        Debug.Log("Checkpoint final alcançado!");

        // Procura o sistema de diálogo do capitão e mostra a mensagem final
        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagemFinal);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointFinal.");
        }
    }

    // Carrega a próxima cena da build após o checkpoint final
    private void CarregarProximaCena()
    {
        // Colocar o nome da próxima cena se preferir, por exemplo: "Fase1"
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
