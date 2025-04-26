using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguidorDeMultiVagao : MonoBehaviour
{
    public Transform locomotiva1;
    public Transform locomotiva2;
    public float distanciaDesejada = 0f;
    public float suavidade = 0f;

    void Update()
    {
        if (locomotiva1 == null || locomotiva2 == null) return;

        // Escolhe a locomotiva mais próxima no eixo X
        Transform alvoMaisProximo =
            Mathf.Abs(transform.position.x - locomotiva1.position.x) <
            Mathf.Abs(transform.position.x - locomotiva2.position.x)
            ? locomotiva1
            : locomotiva2;

        // Calcula direção X
        float direcaoX = Mathf.Sign(transform.position.x - alvoMaisProximo.position.x);
        float posicaoAlvoX = alvoMaisProximo.position.x + direcaoX * distanciaDesejada;

        // Suaviza movimento no eixo X
        float novoX = Mathf.Lerp(transform.position.x, posicaoAlvoX, Time.deltaTime * suavidade);

        // Atualiza posição final
        transform.position = new Vector3(novoX, transform.position.y, transform.position.z);
    }
}
