using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [Header("Referência dos Checkpoints")]
    public GameObject checkpointBox;
    public GameObject checkpointScarecrow;
    public GameObject checkpointStartCircuito;
    public GameObject checkpointFinishCircuito;

    [Header("Mensagens do Capitão")]
    [TextArea]
    public string mensagemBox = "Vamos ver como você se sai com as caixas, elas podem ser movidas e você pode quebrá-las com suas balas.";

    [TextArea]
    public string mensagemScarecrow = "Este é o seu inimigo. Não tenha medo dele, pois ele não terá de você. Botão esquerdo para atirar e mova o mouse para apontar.";

    [TextArea]
    public string mensagemCircuito = "Vamos testar sua agilidade, destrua todos os espantalhos no menor tempo possível.";

    [TextArea]
    public string mensagemFinal = "Parabéns 'Axpira', não foi tão ruim quanto pensei, mas ainda precisamos vencer as nossas missões. Vamos que irei te orientar sobre elas.";

    [Header("Configurações")]
    public float tempoCooldown = 5f;

    private bool boxAtivado = true;
    private bool scarecrowAtivado = true;
    private bool circuitoAtivado = true;
    private bool finalAtivado = true;

    public void AtivarCheckpointBox(Collider2D other)
    {
        if (!boxAtivado || !other.CompareTag("Player")) return;

        print("Checkpoint 1");
        TutorialManager.Instance.MarcarInteragiuComCaixa();
        MostrarMensagem(mensagemBox);

        checkpointBox.SetActive(false);
        boxAtivado = false;
        Invoke(nameof(ReativarBox), tempoCooldown);
    }

    public void AtivarCheckpointScarecrow(Collider2D other)
    {
        if (!scarecrowAtivado || !other.CompareTag("Player")) return;

        print("Checkpoint 2");
        TutorialManager.Instance.MarcarEspantalhoDestruido();
        MostrarMensagem(mensagemScarecrow);

        checkpointScarecrow.SetActive(false);
        scarecrowAtivado = false;
        Invoke(nameof(ReativarScarecrow), tempoCooldown);
    }

    public void AtivarCheckpointCircuito(Collider2D other)
    {
        if (!circuitoAtivado || !other.CompareTag("Player")) return;

        print("Checkpoint 3");
        TutorialManager.Instance.IniciarContagem();
        MostrarMensagem(mensagemCircuito);

        checkpointStartCircuito.SetActive(false);
        circuitoAtivado = false;
        Invoke(nameof(ReativarCircuito), tempoCooldown);
    }

    public void AtivarCheckpointFinal(Collider2D other)
    {
        if (!finalAtivado || !other.CompareTag("Player")) return;

        print("Checkpoint 4");
        TutorialManager.Instance.PararContagem();
        MostrarMensagem(mensagemFinal);

        checkpointFinishCircuito.SetActive(false);
        finalAtivado = false;

        // Congelar player
        Player.isFrozen = true;

        Animator anim = other.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
        }

        Invoke(nameof(CarregarProximaCena), 3f);
    }

    private void MostrarMensagem(string mensagem)
    {
        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagem);
        }
        else
        {
            print("CapitanDialog NÃO encontrado.");
        }
    }

    private void ReativarBox()
    {
        boxAtivado = true;
        if (checkpointBox != null)
            checkpointBox.SetActive(true);
    }

    private void ReativarScarecrow()
    {
        scarecrowAtivado = true;
        if (checkpointScarecrow != null)
            checkpointScarecrow.SetActive(true);
    }

    private void ReativarCircuito()
    {
        circuitoAtivado = true;
        if (checkpointStartCircuito != null)
            checkpointStartCircuito.SetActive(true);
    }

    private void CarregarProximaCena()
    {
        if (checkpointFinishCircuito != null)
            checkpointFinishCircuito.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
