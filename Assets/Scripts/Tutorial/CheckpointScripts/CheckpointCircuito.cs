using UnityEngine;

public class CheckpointCircuito : MonoBehaviour
{
    private bool ativado = false;

    public string mensagemDoCapitao = "Vamos testar sua agilidade, destruá todos os espantalhos no menor tempo possivel.";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ativado || !other.CompareTag("Player")) return;

        ativado = true;
        Debug.Log("Checkpoint do circuito alcançado.");
        TutorialManager.Instance.IniciarContagem();

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            Debug.Log("CapitanDialog encontrado.");
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointCircuito.");
        }
    }
}
