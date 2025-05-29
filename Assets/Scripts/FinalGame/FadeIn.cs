using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed;

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    System.Collections.IEnumerator FadeInRoutine()
    {
        Color color = fadeImage.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // desativa a imagem após fade
    }
}