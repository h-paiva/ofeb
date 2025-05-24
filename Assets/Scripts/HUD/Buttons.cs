using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Manipular as cenas
using UnityEngine.UI; //Manipular a UI do volume

public class Buttons : MonoBehaviour
{
    //Fases Jogaveis

    [Header("Painel de Pausa da Fase")]
    public GameObject PauseMenuPanel; // Referencia ao painel de Menu de Pausa

    public GameObject SettingsMenu; // Referencia ao painel de opções das Fases Jogaveis
    
    //Tutorial
    [Header("Painel de Opções do Menu")]
    public GameObject optionsPanel; // Referencia ao painel de opções do Menu

    

    [Header("Controle de Volume")]
    public Slider volumeSlider; // Referencia ao slider do volume

    
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Se o painel já estiver ativo, fecha. Caso contrário, abre.
            if (PauseMenuPanel != null)
            {
                bool isActive = PauseMenuPanel.activeSelf;
                PauseMenuPanel.SetActive(!isActive);

                // Se estiver ativando o painel de pausa, garantir que o painel de ajustes esteja fechado
                if (!isActive && SettingsMenu != null)
                {
                    SettingsMenu.SetActive(false);
                }
            }
        }
    }


    // BOTOES DO TUTORIAL
    public void QuitGameButton() //Fechar o Game
    {
        Application.Quit();
    }

    public void ReturnToMainMenuButton() //Menu
    {
        print("Reiniciando o Jogo");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void NewGameMenuButton() //Menu
    {
        print("Novo Jogo iniciado!");
        SceneManager.LoadScene("01 - Tutorial", LoadSceneMode.Single); // Cena de tutorial
    }

    public void OptionsButton() //Menu
    {
        Debug.Log("Abrindo Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true); //Faz o Painel de Ajuste aparecer
        }
    }

    public void CloseOptionsButton() //Menu
    {
        Debug.Log("Fechando Ajustes...");
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false); //Faz o Painel de Ajuste voltar ao Menu Principal
        }
    }

    //VOLUME DO GAME
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    //FASES JOGAVEIS
    public void BackGaming() //FasesJogaveis //Voltar ao jogo
    {
        SettingsMenu.SetActive(false);
        PauseMenuPanel.SetActive(false);
    }

    public void OpenSettings() //FasesJogaveis // Abrir os Ajustes
    {
        SettingsMenu.SetActive(true);
        PauseMenuPanel.SetActive(true);
    }

    public void CloseSettings() //FasesJogaveis //Fechar os Ajustes
    {
        SettingsMenu.SetActive(false);
        PauseMenuPanel.SetActive(true);
    }

    public void BackMainMenu()
    {
        print("Voltando ao Menu!");
        SceneManager.LoadScene("00 - Menu Inicial"); // Cena de tutorial
    }

}
