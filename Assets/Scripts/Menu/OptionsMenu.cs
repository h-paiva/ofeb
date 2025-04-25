using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para TextMeshPro

public class OptionsMenu : MonoBehaviour
{
    [Header("Volume Settings")]
    public Slider volumeSlider;

    [Header("Language Settings")]
    public TMP_Dropdown languageDropdown;

    private void Start()
    {
        // Carrega configurações salvas
        LoadSettings();

        // Adiciona os listeners
        volumeSlider.onValueChanged.AddListener(SetVolume);
        languageDropdown.onValueChanged.AddListener(SetLanguage);
    }

    private void LoadSettings()
    {
        // Volume
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("GameVolume");
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }
        else
        {
            // Define volume padrão (ex: 100%)
            volumeSlider.value = 1f;
            AudioListener.volume = 1f;
        }

        // Idioma
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            int savedLanguage = PlayerPrefs.GetInt("GameLanguage");
            languageDropdown.value = savedLanguage;
        }
        else
        {
            // Define idioma padrão (ex: Português)
            languageDropdown.value = 0;
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt("GameLanguage", languageIndex);
        PlayerPrefs.Save();
        Debug.Log("Idioma alterado para: " + languageDropdown.options[languageIndex].text);

        // Se quiser implementar troca de idioma no jogo, pode chamar aqui
        // Tipo: LanguageManager.Instance.SetLanguage(languageIndex);
    }
}
