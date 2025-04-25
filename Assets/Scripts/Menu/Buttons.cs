using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject optionsPanel; // Painel de ajustes

    public void NewGameMenuButton()
    {
        Debug.Log("Novo Jogo iniciado!");
        SceneManager.LoadScene("00-Tutorial", LoadSceneMode.Single);
    }

    public void ReturnToMainMenuButton()
    {
        Debug.Log("Voltando ao Menu Principal...");
        SceneManager.LoadScene("00-MenuInicial", LoadSceneMode.Single);
    }

    public void QuitGameButton()
    {
        Debug.Log("Saindo do Jogo...");
        Application.Quit();
    }

    public void OptionsButton()
    {
        Debug.Log("Abrindo Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptionsButton()
    {
        Debug.Log("Fechando Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }
}
