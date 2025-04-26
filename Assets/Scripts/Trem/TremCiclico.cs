using UnityEngine;
using System.Collections;

public class TremCiclico : MonoBehaviour
{
    [Header("Pontos para Esquerda")]
    public Transform pontoAEsquerda;
    public Transform pontoBEsquerda;

    [Header("Pontos para Direita")]
    public Transform pontoADireita;
    public Transform pontoBDireita;


    [Header("Configurações")]
    public float velocidade = 2f;
    public float tempoInicial = 10f;
    public float tempoEntrePontos = 5f;

    private bool indoParaDireita = false;

    void Start()
    {
        StartCoroutine(CicloTrem());
    }

    IEnumerator CicloTrem()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoInicial);

            if (indoParaDireita)
            {
                Debug.Log("Indo para a direita, parada no primeiro ponto");
                yield return StartCoroutine(MoverAte(pontoADireita.position));
                yield return new WaitForSeconds(tempoEntrePontos);
                Debug.Log("Indo para a direita, parada no segundo ponto");
                yield return StartCoroutine(MoverAte(pontoBDireita.position));
            }
            else
            {
                Debug.Log("Indo para a esquerda, parada no primeiro ponto");
                yield return StartCoroutine(MoverAte(pontoAEsquerda.position));
                yield return new WaitForSeconds(tempoEntrePontos);
                Debug.Log("Indo para a esquerda, parada no segundo ponto");
                yield return StartCoroutine(MoverAte(pontoBEsquerda.position));
            }

            indoParaDireita = !indoParaDireita;
        }
    }

    IEnumerator MoverAte(Vector3 destino)
    {
        while (Vector3.Distance(transform.position, destino) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
            yield return null;
        }

        transform.position = destino;
    }
}
