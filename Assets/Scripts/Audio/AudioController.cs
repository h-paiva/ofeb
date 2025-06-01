using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo; //Referente a trilha das Fases
    public AudioClip[] musicasDeFundo;
    // Start is called before the first frame update
    void Start()
    {
        int IndexDaMusicaDeFundo = Random.Range(0, musicasDeFundo.Length);
        AudioClip musicaDeFundo = musicasDeFundo[IndexDaMusicaDeFundo]; //Paras receber as musicas indexada nas fases
        AudioClip fundoDaCenaRJ = musicasDeFundo[IndexDaMusicaDeFundo]; //Para receber as musicas de som ambiente
        audioSourceMusicaDeFundo.clip = musicaDeFundo;
        //Para tocar os audios
        audioSourceMusicaDeFundo.Play();
    }
}
