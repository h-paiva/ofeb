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
        SceneManager.LoadScene("01-rio-de-janeiro", LoadSceneMode.Single); // Cena de tutorial
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

    public void Pause()//Abrindo o Menu
    {
        Player.isFrozen = true; //Freza o player ao abrir o menu
        Enemy.isFrozen = true; //Freza o enemy ao abrir o menu
        PlayerAimAndShoot.isFrozen = true; //Freza o braço ao abrir o menu
        EnemyAimAndShoot.isFrozen = true; //Freza o braço do inigo ao abrir o menu
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
            anim.SetBool("idle", true);
        }
    }

    public void BackGaming() //FasesJogaveis //Voltar ao jogo
    {
        Player.isFrozen = false; //Volta ao normal quando sai do menu
        Enemy.isFrozen = false; //Volta ao normal quando sai do menu
        PlayerAimAndShoot.isFrozen = false; //Volta ao normal quando sai do menu
        EnemyAimAndShoot.isFrozen = false; //Volta ao normal quando sai do menu
        SettingsMenu.SetActive(false);
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("walk", true);
            anim.SetBool("run", true);
            anim.SetBool("jump", true);
            anim.SetBool("idle", true);
        }
    }

    public void OpenSettings() //FasesJogaveis // Abrir os Ajustes
    {
        PauseMenuPanel.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void CloseSettings() //FasesJogaveis //Fechar os Ajustes
    {
        SettingsMenu.SetActive(false);
        PauseMenuPanel.SetActive(true);
    }

    public void BackMainMenu()
    {
        print("Voltando ao Menu!");
        SceneManager.LoadScene("00-menu"); // Cena de tutorial
    }

}