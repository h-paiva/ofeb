using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    private Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();

        if (dropdown != null)
        {
            dropdown.onValueChanged.AddListener(OnLanguageChanged);

            // Carrega o idioma salvo
            int savedLanguage = PlayerPrefs.GetInt("GameLanguage", 0);
            dropdown.value = savedLanguage;
            dropdown.RefreshShownValue();
        }
    }

    private void OnLanguageChanged(int index)
    {
        PlayerPrefs.SetInt("GameLanguage", index);
        PlayerPrefs.Save();
        LocalizationManager.instance.LoadLanguage(index);
    }
}
