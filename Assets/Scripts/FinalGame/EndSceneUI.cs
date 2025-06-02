using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("00-menu"); // nome da sua cena inicial
    }

    public void Recomecar()
    {
        SceneManager.LoadScene("IntroGame1"); // Funciona só em build
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo"); // Funciona só em build
    }
}