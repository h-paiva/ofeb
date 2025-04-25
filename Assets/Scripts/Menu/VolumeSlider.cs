using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnVolumeChanged);

            // Carrega o volume salvo
            float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
            slider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GameVolume", value);
        PlayerPrefs.Save();
    }
}
