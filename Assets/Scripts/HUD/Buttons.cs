using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void QuitGameButton()
    {
        Application.Quit();
    }
    public void ReturnToMainMenuButton()
    {
        Debug.Log("Reiniciando o Jogo");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void NewGameMenuButton()
    {
        Debug.Log("Reiniciando o Jogo");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void OptionsButton()
    {
        Debug.Log("Reiniciando o Jogo");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
