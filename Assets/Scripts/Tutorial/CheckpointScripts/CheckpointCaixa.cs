using UnityEngine;

public class CheckpointCaixa : MonoBehaviour
{
    private bool podeAtivar = true;
    public float tempoCooldown = 5f;

    public string mensagemDoCapitao = "Use as caixas com sabedoria! São parte do ambiente.";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !podeAtivar) return;

        Debug.Log("Checkpoint da caixa alcançado.");
        TutorialManager.Instance.MarcarInteragiuComCaixa();

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            Debug.Log("CapitanDialog encontrado.");
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointCaixa.");
        }

        podeAtivar = false;
        Invoke(nameof(ReativarCheckpoint), tempoCooldown);
    }

    private void ReativarCheckpoint()
    {
        podeAtivar = true;
    }
}
