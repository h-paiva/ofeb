using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControll : MonoBehaviour
{
    public AudioClip myClip;  

    void Start()
    {
        if (myClip != null)
        {
            // Cria um AudioSource temporário
            AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();

            // Configura o AudioSource
            tempAudioSource.clip = myClip;
            tempAudioSource.loop = true;     // Ativa o loop
            tempAudioSource.playOnAwake = false;

            // Toca o áudio
            tempAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource não está atribuído!");
        }
    }
}
