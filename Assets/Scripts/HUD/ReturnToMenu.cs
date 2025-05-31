using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMenu : MonoBehaviour
{
    void Start()
    {
        // Connect the button click event via code
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ReturnToMainMenu);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("00 - Menu Inicial");
    }
}
