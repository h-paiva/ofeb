using UnityEngine;

public class CheckpointScarecrow : MonoBehaviour
{
    [Header("Mensagem do Capitão")]
    [TextArea]
    [SerializeField] private string mensagemDoCapitao = 
        "Este e o seu inimigo. Nao tenha medo dele, pois ele não tera de você. Botao esquerdo para atirar e mova o mouse para apontar.";

    [Header("Configurações")]
    [SerializeField] private float tempoCooldown = 5f;

    private bool mensagemMostrada = false;
    private bool podeAtivar = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !podeAtivar || mensagemMostrada) return;

        Debug.Log("Checkpoint do espantalho alcançado.");
        TutorialManager.Instance.MarcarEspantalhoDestruido();

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
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
