using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraConfig : MonoBehaviour
{
    private string positionPlayer =  "right";
    private Vector3 targetPosition; // Posição alvo para onde a câmera vai se mover
    private bool starFollow = false;

    [Header("Suavização da Camera")]
    public float smoothSpeed = 5f; // Quanto maior, mais rápido a câmera alcança o alvo

    public enum Fase
    {
        Tutorial,
        Fase1,
        Fase2
    }
    [Header("Fase")]
    public Fase fase;

    [Header("Alvo")]
    [SerializeField] public Transform player;

    [Header("Limites da Câmera no Eixo X")]
    public float minX = -10f;
    public float maxX = 100f;

    [Header("Offset")]
    public Vector3 offset;

    public void CameraFollowPlayerActive(bool starFollowPlayer)
    {
        starFollow = starFollowPlayer;
    }
    public void CameraFollowPlayer(string position)
    {
        positionPlayer = position;

        if (fase == Fase.Fase1 && starFollow)
        {
            if (positionPlayer  == "right") 
            {
                targetPosition = new Vector3(player.position.x + 86, player.position.y +2, player.position.z);
                transform.position = Vector2.Lerp(targetPosition, player.position, player.position.z);
            } else 
            {
                targetPosition = new Vector3(player.position.x + 82, player.position.y +2, player.position.z);
                transform.position = Vector2.Lerp(targetPosition, player.position, player.position.z);
            }
        }
    }
    void LateUpdate()
    {
        if (fase == Fase.Fase2)
        {
            // Posição alvo da câmera
            Vector3 targetPosition = transform.position;

            if (positionPlayer == "right")
            {
                targetPosition.x = Mathf.Clamp(player.position.x + offset.x + 4, minX, maxX);
            }
            else if (positionPlayer == "left")
            {
                targetPosition.x = Mathf.Clamp(player.position.x + offset.x - 4, minX, maxX);
            }

            // Mantém os outros eixos com o offset
            targetPosition.y = player.position.y + offset.y;
            targetPosition.z = player.position.z + offset.z;

            // Suaviza o movimento
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

}
