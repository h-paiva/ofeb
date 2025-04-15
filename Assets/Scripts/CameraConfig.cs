using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private string positionPlayer = "right";
    private Vector3 targetPosition;
    private bool starFollow = false;

    private float cameraZ; // Valor fixo do Z da câmera

    private void Start()
    {
        cameraZ = transform.position.z;
        if (Player == null)
        {
            Debug.LogError("⚠️ 'Player' não está atribuído no CameraConfig! A câmera não poderá seguir o jogador.");
            return;
        }

        cameraZ = transform.position.z; // Salva o Z atual da câmera
    }

    public void CameraFollowPlayerActive(bool starFollowPlayer)
    {
        starFollow = starFollowPlayer;
    }

    public void CameraFollowPlayer(string position)
    {
        if (!starFollow || Player == null) return;

    positionPlayer = position;

    if (positionPlayer == "right")
    {
        targetPosition = new Vector3(Player.position.x + 86f, Player.position.y + 2f, cameraZ);
    }
    else
    {
        targetPosition = new Vector3(Player.position.x + 82f, Player.position.y + 2f, cameraZ);
    }

    // Limites do cenário
    float minX = 0f;
    float maxX = 100f;
    float minY = -5f;
    float maxY = 5f;

    targetPosition = new Vector3(
        Mathf.Clamp(targetPosition.x, minX, maxX),
        Mathf.Clamp(targetPosition.y, minY, maxY),
        targetPosition.z
    );

    // movimento suave
    transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
}
