using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Transform pauseMenu;
    [SerializeField] private Transform optionMenu;
    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void OptionsGame()
    {
        pauseMenu.gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnMainMenu()
    {
        optionMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        Debug.Log("Reiniciando o Jogo");
    }
}
