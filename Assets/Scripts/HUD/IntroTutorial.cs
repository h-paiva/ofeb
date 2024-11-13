using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutorial : MonoBehaviour
{
    [SerializeField] public float speed; // Velocidade de movimento da câmera
    [SerializeField] public float timerIntroMax; // Velocidade de movimento da câmera
    [SerializeField] private Vector3 targetPosition; // Posição alvo para onde a câmera vai se mover
    [SerializeField] private Vector3 startPosition; // Posição inicial da câmera
    [SerializeField] private GameObject Warning; 
    [SerializeField] private GameObject LifeHUD; 


    // Start is called before the first frame update
    private  bool introStart = false;
    private  bool introFinish = false;

    private void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.Space)){
            StartIntroGame();
            Destroy(Warning, 0);
        }
        if(introStart & !introFinish)
        {
            startPosition = transform.position; // Salva a posição inicial da câmera
            targetPosition = new Vector3(startPosition.x + 90, startPosition.y, startPosition.z); // Define a posição alvo
            transform.position = Vector3.Lerp(startPosition, targetPosition, speed);
        }
    }
    public void StartIntroGame()
    {   
        IntroGame();
        Invoke("EndIntroduction", timerIntroMax);
    }
    public void IntroGame()
    {   
        if (introStart) return; // Evita que o método seja chamado múltiplas vezes
        introStart = true;
    }
    void EndIntroduction()
    {
        introFinish = true;
        LifeHUD.gameObject.SetActive(true);
    }
}
