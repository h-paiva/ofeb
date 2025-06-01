using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuNavegation : MonoBehaviour
{   
    public GameObject optionsPanel;
    
    public void NewGameMenuButton() //Menu
    {
        print("Novo Jogo!");
        SceneManager.LoadScene("01-rio-de-janeiro");
    }

    public void OptionsButton() //Menu
    {
        Debug.Log("Abrindo Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true); //Faz o Painel de Ajuste aparecer
        }
    }

    public void QuitGameButton()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
