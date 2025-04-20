using UnityEngine;

public class TremMover : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float distanciaAlvo = 10f;
    public float velocidade = 2f;
    public bool moverParaDireita = false;

    [Header("Ativação por Proximidade")]
    public Transform jogador;
    public float raioAtivacao = 5f;

    private Vector3 pontoInicial;
    private Vector3 pontoFinal;
    private bool emMovimento = false;

    void Start()
    {
        pontoInicial = transform.position;

        // Define o ponto final com base na direção
        float deslocamento = moverParaDireita ? distanciaAlvo : -distanciaAlvo;
        pontoFinal = pontoInicial + new Vector3(deslocamento, 0f, 0f);
    }

    void Update()
    {
        // Verifica se o jogador está dentro do raio para ativar
        if (!emMovimento && jogador != null)
        {
            float distancia = Vector3.Distance(jogador.position, transform.position);
            if (distancia <= raioAtivacao)
            {
                emMovimento = true;
            }
        }

        // Move o trem continuamente em direção ao destino
        if (emMovimento)
        {
            transform.position = Vector3.MoveTowards(transform.position, pontoFinal, velocidade * Time.deltaTime);

            // Quando o trem chegar ao destino, para o movimento
            if (Vector3.Distance(transform.position, pontoFinal) <= 0.01f)
            {
                transform.position = pontoFinal;
                emMovimento = false;
            }
        }
    }
}
