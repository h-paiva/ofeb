using UnityEngine;

public class Bombardeiro : MonoBehaviour
{
    public float velocidade = 3f;
    public GameObject bombaPrefab;
    public Transform pontoDeSoltarBomba;

    public Transform pontoEsquerdo; // Limite esquerdo
    public Transform pontoDireito; // Limite direito

    public float toleranciaParaSoltar = 0.5f;
    public float tempoEntreBombas = 2f;

    public Transform player; // Jogador
    private float tempoDesdeUltimaBomba;
    private bool indoParaDireita = true;

    void Start()
    {
        // Busca o jogador pela tag
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null)
        {
            player = jogador.transform;
        }
    }

    void Update()
    {
        if (player == null || pontoEsquerdo == null || pontoDireito == null)
            return;

        tempoDesdeUltimaBomba += Time.deltaTime;

        // Define direção com base no sentido atual
        float direcao = indoParaDireita ? 1 : -1;

        // Move o bombardeiro horizontalmente
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        // Verifica limites
        if (transform.position.x >= pontoDireito.position.x)
            indoParaDireita = false;
        else if (transform.position.x <= pontoEsquerdo.position.x)
            indoParaDireita = true;

        // Verifica se está em cima do jogador
        if (Mathf.Abs(transform.position.x - player.position.x) < toleranciaParaSoltar &&
            tempoDesdeUltimaBomba >= tempoEntreBombas)
        {
            SoltarBomba();
            tempoDesdeUltimaBomba = 0f;
            Debug.Log("Soltou a bomba!");
        }
    }

    void SoltarBomba()
    {
        Instantiate(bombaPrefab, pontoDeSoltarBomba.position, Quaternion.identity);
    }
}
