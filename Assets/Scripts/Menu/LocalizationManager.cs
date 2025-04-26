using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        LoadLanguage(PlayerPrefs.GetInt("GameLanguage", 0));
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndAssignTexts();
        UpdateTexts();
    }

    public void LoadLanguage(int languageIndex)
    {
        if (localizedText == null)
            localizedText = new Dictionary<string, string>();
        else
            localizedText.Clear();

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
        if (localizedText != null && localizedText.ContainsKey(key))
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

    // >>>> AQUI: Esse método cria um LocalizationManager automático se não existir <<<<
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateLocalizationManagerIfNotExists()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("LocalizationManager");
            obj.AddComponent<LocalizationManager>();
        }
    }
}
