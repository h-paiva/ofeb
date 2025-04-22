using UnityEngine;

public class CheckpointFinal : MonoBehaviour
{
    private bool jaFinalizou = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (jaFinalizou) return;

        if (collision.CompareTag("Player"))
        {
            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.PararContagem();
                jaFinalizou = true;
            }

            Debug.Log("Checkpoint final alcançado!");

            CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
            if (capitanDialog != null)
            {
                capitanDialog.MostrarMensagemTemporaria("Parabéns! Você concluiu o tutorial.");
            }
            else
            {
                Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointFinal.");
            }
        }
    }
}
