using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutorial : MonoBehaviour
{
    [SerializeField] public float speed = 0.2f;
    [SerializeField] public float timerIntroMax = 5f;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private GameObject Warning;
    [SerializeField] private GameObject LifeHUD;

    private bool introStart = false;
    private bool introFinish = false;

    public CameraConfig cameraConfig;

    private Vector3 startPosition;
    private float progress = 0f;

    private void Start()
    {
        cameraConfig = FindObjectOfType<CameraConfig>();
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x + 90, startPosition.y, startPosition.z); // Define a posição alvo da câmera
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartIntroGame();
        }

        if (introStart && !introFinish)
        {
            progress += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
        }
    }

    public void StartIntroGame()
    {
        if (introStart) return;
        introStart = true;
        Destroy(Warning);
        Invoke("EndIntroduction", timerIntroMax);
    }

    void EndIntroduction()
    {
        introFinish = true;
        LifeHUD.gameObject.SetActive(true);
        cameraConfig.CameraFollowPlayerActive(true);
    }
}
