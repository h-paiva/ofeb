using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false); // Esconde no início
    }

    public void ShowGameOver()
    {
        Player.isFrozen = true; //Freza o player qnd morre
        PlayerAimAndShoot.isFrozen = true; //Freza o tiro
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
    }

    public void RestartGame()
    {
        Player.isFrozen = false; //Freza o player qnd morre
        PlayerAimAndShoot.isFrozen = false; //Freza o tiro
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMenu()
    {
        Player.isFrozen = false; //Freza o player qnd morre
        PlayerAimAndShoot.isFrozen = false; //Freza o tiro
        Time.timeScale = 1f;
        SceneManager.LoadScene("00-menu"); // Altere se o nome do menu for outro
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo"); // Funciona só em build
    }
}