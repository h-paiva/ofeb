using System;
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

    //[Header("Camera")]
    //public Camera camera; // Quanto maior, mais rápido a câmera alcança o alvo

    public enum Fase
    {
        Tutorial,
        Fase1,
        Fase2
    }
    [Header("Fase")]
    public Fase fase;

    [Header("Player")]
    [SerializeField] public Transform player;

    [Header("Boss Final Fase Position")]
    [SerializeField] public Transform bossFinalPositionCamera;

    [Header("Limites da Câmera no Eixo X")]
    public float minX = -10f;
    public float maxX = 100f;

    [Header("Offset")]
    public Vector3 offset;

    private bool bossFinalFase2 = false;
    public void CameraFollowPlayerActive(bool starFollowPlayer)
    {
        starFollow = starFollowPlayer;
    }
    public void CameraFollowPlayer(string position)
    {
        positionPlayer = position;

        if (fase == Fase.Fase1 && starFollow)
        {
            // Posição alvo da câmera
            Vector3 targetPosition = transform.position ;

            if (positionPlayer == "right")
            {
                targetPosition.x = Mathf.Clamp(player.position.x + 82 + offset.x + 4, minX, maxX);
            }
            else if (positionPlayer == "left")
            {
                targetPosition.x = Mathf.Clamp(player.position.x + 82 + offset.x - 4, minX, maxX);
            }

            // Mantém os outros eixos com o offset
            targetPosition.y = player.position.y + 2 + offset.y;
            targetPosition.z = player.position.z + offset.z;

            // Suaviza o movimento
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * (smoothSpeed / 2));
        }
    }
    void LateUpdate()
    {
        if (fase == Fase.Tutorial)
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * (smoothSpeed / 2));
        }
        if (fase == Fase.Fase2 && bossFinalFase2 == false)
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * (smoothSpeed/2));
        }
        else if (fase == Fase.Fase2 && bossFinalFase2 == true)
        {
            // Posição alvo da câmera
            Vector3 targetPosition = transform.position;

            // Mantém os outros eixos com o offset
            targetPosition.z = player.position.z + offset.z;

            targetPosition.x = Mathf.Clamp(bossFinalPositionCamera.position.x + offset.x , minX, maxX);
            targetPosition.y = Mathf.Clamp(bossFinalPositionCamera.position.y + offset.y , minX, maxX);

            // Suaviza o movimento
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

            Camera.main.orthographicSize = 10f;
        }
    }
    public void SetBossFinalFase2( bool finalBossActive )
    {
        bossFinalFase2 = finalBossActive;
    }
}
