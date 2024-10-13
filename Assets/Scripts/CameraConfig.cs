using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    public float speed = 1.0f; // Velocidade de movimento da câmera
    private Vector3 targetPosition; // Posição alvo para onde a câmera vai se mover

    private Vector3 startPosition; // Posição inicial da câmera

    void Start()
    {
        startPosition = transform.position; // Salva a posição inicial da câmera
        targetPosition = new Vector3(startPosition.x + 35, startPosition.y, startPosition.z); // Define a posição alvo
    }

    void Update()
    {
        // Move a câmera em direção à posição alvo
        transform.position = Vector3.Lerp(startPosition, targetPosition, Time.time * speed);
    }
}
