using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("Progresso do Tutorial")]
    public bool espantalhoDestruido = false;
    public bool interagiuComCaixa = false;
    public bool circuitoConcluido = false;

    [Header("Estatísticas do Circuito")]
    public float tempoCircuito = 0f;
    public int caixasUsadas = 0;
    public int balasDisparadas = 0;

    private bool contandoTempo = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Pode remover o DontDestroyOnLoad se não quiser manter entre cenas
        // DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (contandoTempo)
        {
            tempoCircuito += Time.deltaTime;
        }
    }

    // === Ações públicas para serem chamadas por outros scripts ===

    public void IniciarContagem()
    {
        if (contandoTempo) return;

        tempoCircuito = 0f;
        balasDisparadas = 0;
        caixasUsadas = 0;
        contandoTempo = true;

        Debug.Log("Contagem de tempo do circuito iniciada!");
    }

    public void PararContagem()
    {
        if (!contandoTempo) return;

        contandoTempo = false;
        circuitoConcluido = true;

        Debug.Log($"Circuito concluído!");
        Debug.Log($"Tempo: {tempoCircuito:F2} segundos");
        Debug.Log($"Balas disparadas: {balasDisparadas}");
        Debug.Log($"Caixas usadas: {caixasUsadas}");
    }

    public void RegistrarBala()
    {
        if (contandoTempo)
        {
            balasDisparadas++;
        }
    }

    public void RegistrarCaixa()
    {
        if (contandoTempo)
        {
            caixasUsadas++;
        }
    }

    public void MarcarEspantalhoDestruido()
    {
        espantalhoDestruido = true;
        Debug.Log("Tutorial: Espantalho destruído");
    }

    public void MarcarInteragiuComCaixa()
    {
        interagiuComCaixa = true;
        Debug.Log("Tutorial: Interagiu com a caixa");
    }
}
