using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public enum TipoCheckpoint { Caixa, Scarecrow, Circuito, Final }
    public TipoCheckpoint tipo;

    private CheckpointManager manager;
    private bool playerDentro = false;

    private void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
        if (manager == null)
            Debug.LogError("CheckpointManager não encontrado na cena.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || playerDentro) return;

        playerDentro = true;

        switch (tipo)
        {
            case TipoCheckpoint.Caixa:
                manager.AtivarCheckpointBox(other);
                break;
            case TipoCheckpoint.Scarecrow:
                manager.AtivarCheckpointScarecrow(other);
                break;
            case TipoCheckpoint.Circuito:
                manager.AtivarCheckpointCircuito(other);
                break;
            case TipoCheckpoint.Final:
                manager.AtivarCheckpointFinal(other);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerDentro = false;
    }
}
