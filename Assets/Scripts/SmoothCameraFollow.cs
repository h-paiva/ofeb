using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Alvo a Seguir")]
    public Transform player;

    [Header("Configuração de Offset")]
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    [Header("Configuração de Suavização")]
    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;

    [Header("Limites da Câmera")]
    public float minX = -50f;
    public float maxX = 50f;
    public float minY = -10f;
    public float maxY = 10f;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Jogador não atribuído na câmera.");
            return;
        }

        // Posição desejada
        Vector3 desiredPosition = player.position + offset;

        // Aplica os limites
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Suaviza o movimento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
