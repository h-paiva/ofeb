using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeLevel;
    [SerializeField] private GameObject painelInicial;
    [SerializeField] private GameObject painelOpcoes;

    public Image fadeImage;
    public float fadeSpeed;
    public void Jogar()
    {
        print("Novo Jogo!");

        StartCoroutine(FadeInRoutine(2f));
        SceneManager.LoadScene(nomeLevel);
    }

    System.Collections.IEnumerator FadeInRoutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Color color = fadeImage.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // desativa a imagem após fade
    }

    public void AbrirOpcoes()
    {
        Debug.Log("Abrir Opções");
        painelInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes() 
    {
        Debug.Log("Fechar Opções");
        painelOpcoes.SetActive(false);
        painelInicial.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
