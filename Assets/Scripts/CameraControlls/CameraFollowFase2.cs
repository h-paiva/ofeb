using UnityEngine;

public class CameraFollowFase2 : MonoBehaviour
{
    [Header("Alvo")]
    public Transform player;

    [Header("Limites da Câmera no Eixo X")]
    public float minX = -10f;
    public float maxX = 100f;

    [Header("Offset")]
    public Vector3 offset;

    void LateUpdate()
    {
        if (player == null)
            return;

        // Pega a posição atual da câmera
        Vector3 newPosition = transform.position;

        // Atualiza a posição X da câmera para seguir o jogador com limites
        newPosition.x = Mathf.Clamp(player.position.x + offset.x, minX, maxX);

        // Mantém os outros eixos com o offset
        newPosition.y = player.position.y + offset.y;
        newPosition.z = player.position.z + offset.z;

        // Aplica a nova posição
        transform.position = newPosition;
    }
}
