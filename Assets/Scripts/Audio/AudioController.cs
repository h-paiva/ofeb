using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo;
    public AudioSource audioSourceSFX;

    public AudioClip[] musicasDeFundo;
    // Start is called before the first frame update
    void Start()
    {
        int IndexDaMusicaDeFundo = Random.Range(0, musicasDeFundo.Length);
        AudioClip musicaDeFundoFase1 = musicasDeFundo[IndexDaMusicaDeFundo];
        audioSourceMusicaDeFundo.clip = musicaDeFundoFase1;
        audioSourceMusicaDeFundo.Play();
    }

    public void ToqueSFX(AudioClip clip)
    {
        AudioClip audioSourceSFX = clip;
        audioSourceMusicaDeFundo.Play();
    }
}
