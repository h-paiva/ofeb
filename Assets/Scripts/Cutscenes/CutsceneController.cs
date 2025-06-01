using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoClip do IntroGame1
    public string nomeCenaTutorial = "01-rio-de-janeiro";
    // Start is called before the first frame update
    void Start()
    {
        // Quando o vídeo terminar, chama a função OnVideoFinished
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nomeCenaTutorial);
    }
}
