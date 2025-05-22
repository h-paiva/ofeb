using UnityEngine;

public class CheckpointCircuito : MonoBehaviour
{
    [Header("Mensagem do Capitão")]
    [TextArea]
    [SerializeField] private string mensagemDoCapitao = 
        "Vamos testar sua agilidade, destrua todos os espantalhos no menor tempo possível.";

    [Header("Configurações")]
    [SerializeField] private float tempoCooldown = 5f;

    private bool mensagemMostrada = false;
    private bool podeAtivar = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !podeAtivar || mensagemMostrada) return;

        Debug.Log("Checkpoint do circuito alcançado.");
        TutorialManager.Instance.IniciarContagem();

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointCircuito.");
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
