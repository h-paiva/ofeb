using UnityEngine;

public class CheckpointCaixa : MonoBehaviour
{
    [Header("Mensagem do Capitão")]
    [TextArea]
    [SerializeField] private string mensagemDoCapitao = 
        "Vamos ver como você se sai com as caixas, elas podem ser movidas e você pode quebrá-las com suas balas.";

    [Header("Configurações")]
    [SerializeField] private float tempoCooldown = 5f;

    private bool mensagemMostrada = false;
    private bool podeAtivar = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !podeAtivar || mensagemMostrada) return;

        print("Checkpoint 1");
        TutorialManager.Instance.MarcarInteragiuComCaixa();

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointCaixa.");
        }

        mensagemMostrada = true;
        podeAtivar = false;
        Invoke(nameof(ReativarCheckpoint), tempoCooldown);
    }

    private void ReativarCheckpoint()
    {
        podeAtivar = true;
    }
}
