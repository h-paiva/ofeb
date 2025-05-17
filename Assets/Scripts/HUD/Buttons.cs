using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Manipular as cenas
using UnityEngine.UI; //Manipular a UI / aqui foi o volume

public class Buttons : MonoBehaviour
{
    [Header("Painel de Opções")]
    public GameObject optionsPanel; // Referência ao painel de opções

    [Header("Controle de Volume")]
    public Slider volumeSlider; // Referência ao slider do volume

    void Start()
    {
        //Tentando configurar o volume do game
        if (volumeSlider != null)
        {
            volumeSlider.value = 1f; // Volume 100% no início
            AudioListener.volume = 1f;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }


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
        Debug.Log("Novo Jogo iniciado!");
        SceneManager.LoadScene("01 - Tutorial", LoadSceneMode.Single); // Cena de tutorial
    }

    public void OptionsButton()
    {
        Debug.Log("Abrindo Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true); //Faz o Painel de Ajuste aparecer
        }
    }

    public void CloseOptionsButton()
    {
        Debug.Log("Fechando Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false); //Faz o Painel de Ajuste voltar ao Menu Principal
        }
    }
     public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    

}
