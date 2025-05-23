using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Texto da contagem regressiva")]
    public Text textoContagemRegressiva;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void MostrarContagemRegressiva(int segundos)
    {
        StartCoroutine(ContagemRegressivaCoroutine(segundos));
    }

    private IEnumerator ContagemRegressivaCoroutine(int segundos)
    {
        textoContagemRegressiva.gameObject.SetActive(true);

        while (segundos > 0)
        {
            textoContagemRegressiva.text = segundos.ToString();
            yield return new WaitForSeconds(1f);
            segundos--;
        }

        textoContagemRegressiva.text = "Vai";
        yield return new WaitForSeconds(1f);

        textoContagemRegressiva.gameObject.SetActive(false);
    }
}
