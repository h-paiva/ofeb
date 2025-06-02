using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController2 : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoClip do IntroGame1
    public string nomeCenaFase2 = "02-napolis";
    // Start is called before the first frame update
    void Start()
    {
        // Quando o vídeo terminar, chama a função OnVideoFinished
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nomeCenaFase2);
    }
}
