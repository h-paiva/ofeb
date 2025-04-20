using UnityEngine;

public class SeguidorDeVagao : MonoBehaviour
{
    public Transform alvoParaSeguir; // Quem o vagão vai seguir
    public float distanciaDesejada = 2f; // Distância entre os vagões
    public float suavidade = 5f; // Suavização do movimento

    void Update()
    {
        if (alvoParaSeguir == null) return;

        // Calcula a direção no eixo X apenas
        float direcaoX = Mathf.Sign(transform.position.x - alvoParaSeguir.position.x);
        float posicaoAlvoX = alvoParaSeguir.position.x + direcaoX * distanciaDesejada;

        // Suaviza o movimento apenas no X
        float novoX = Mathf.Lerp(transform.position.x, posicaoAlvoX, Time.deltaTime * suavidade);

        // Aplica a nova posição mantendo Y e Z
        transform.position = new Vector3(novoX, transform.position.y, transform.position.z);
    }
}
