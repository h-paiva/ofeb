using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // <- Mudado para UI.Text
using UnityEngine.SceneManagement;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    [Header("Textos de UI")]
    public Text newGameText;
    public Text optionsText;
    public Text exitText;

    private Dictionary<string, string> localizedText;
    private string currentLanguage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLanguage(PlayerPrefs.GetInt("GameLanguage", 0));
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndAssignTexts();
        UpdateTexts();
    }

    public void LoadLanguage(int languageIndex)
    {
        localizedText = new Dictionary<string, string>();

        switch (languageIndex)
        {
            case 0:
                currentLanguage = "Portuguese";
                break;
            case 1:
                currentLanguage = "English";
                break;
            case 2:
                currentLanguage = "Italian";
                break;
            default:
                currentLanguage = "Portuguese";
                break;
        }

        TextAsset textFile = Resources.Load<TextAsset>("Languages/" + currentLanguage);

        if (textFile != null)
        {
            string[] lines = textFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (line.Contains("="))
                {
                    string[] keyValue = line.Split('=');
                    if (keyValue.Length == 2)
                    {
                        localizedText[keyValue[0].Trim()] = keyValue[1].Trim();
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Arquivo de idioma não encontrado: " + currentLanguage);
        }

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        if (newGameText != null)
            newGameText.text = GetLocalizedValue("new_game");

        if (optionsText != null)
            optionsText.text = GetLocalizedValue("options");

        if (exitText != null)
            exitText.text = GetLocalizedValue("exit");
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            Debug.LogWarning("Chave de tradução não encontrada: " + key);
            return key;
        }
    }

    private void FindAndAssignTexts()
    {
        if (newGameText == null)
            newGameText = GameObject.Find("NewGameText")?.GetComponent<Text>();

        if (optionsText == null)
            optionsText = GameObject.Find("OptionsText")?.GetComponent<Text>();

        if (exitText == null)
            exitText = GameObject.Find("ExitText")?.GetComponent<Text>();
    }
}