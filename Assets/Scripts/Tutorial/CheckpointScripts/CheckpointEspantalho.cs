using UnityEngine;

public class CheckpointScarecrow : MonoBehaviour
{
    private bool ativado = false;

    public string mensagemDoCapitao = "Este e o seu inimigo. Não tenha do dele, pois ele nao terá de voce Botao esquerdo para atirar e mova o mouse para apontar";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ativado || !other.CompareTag("Player")) return;

        ativado = true;
        Debug.Log("Checkpoint do espantalho alcançado.");
        TutorialManager.Instance.MarcarEspantalhoDestruido();

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            Debug.Log("CapitanDialog encontrado.");
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointScarecrow.");
        }
    }
}
