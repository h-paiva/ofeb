using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] private Transform endPosition;
    [SerializeField] public float speed; // Velocidade de movimento da câmera
    [SerializeField] public int timerIntroMax; // Velocidade de movimento da câmera
    [SerializeField] private Vector3 targetPosition; // Posição alvo para onde a câmera vai se mover
    [SerializeField] private Vector3 startPosition; // Posição inicial da câmera
    private  bool initialCamera = false;
    
    void Start()
    {   
        startPosition = transform.position; // Salva a posição inicial da câmera
        targetPosition = new Vector3(startPosition.x + 30, startPosition.y, startPosition.z); // Define a posição alvo
        Invoke("EndIntroduction", timerIntroMax);
    }
    private void FixedUpdate() 
    {
        if(initialCamera == false)
        {
            // Move a câmera em direção à posição alvo
            transform.position = Vector3.Lerp(startPosition, targetPosition, Time.time * speed);
        }
        if(initialCamera == true)
        {   
            //controle da posição do personagem a partir do momento que a camera segue ele
            targetPosition = new Vector3(Player.position.x + 31, Player.position.y +2, Player.position.z);
            transform.position = Vector2.Lerp(targetPosition, Player.position, 0.01f);
        }
    }
    void EndIntroduction()
    {
        initialCamera = true;
    }
}
