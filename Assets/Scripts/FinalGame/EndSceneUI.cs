using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("00 - Menu Inicial"); // nome da sua cena inicial
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}