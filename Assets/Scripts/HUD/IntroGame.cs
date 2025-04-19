// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameStarted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre cenas se necessário
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
