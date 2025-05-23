using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointCircuito : MonoBehaviour
{
    [Header("Mensagem do Capitão")]
    [TextArea]
    [SerializeField]
    private string mensagemDoCapitao =
        "Vamos testar sua agilidade, destrua todos os espantalhos no menor tempo possível.";

    [Header("Configurações")]
    [SerializeField] private float tempoCooldown = 5f;

    [Header("UI Contagem Regressiva")]
    [SerializeField] private Text textoContagem;

    [Header("Referências do Player")]
    [SerializeField] private GameObject player;
    private MonoBehaviour scriptMovimento;

    [Header("Relógio")]
    [SerializeField] private GameObject relogioUI;
    [SerializeField] private AudioSource somRelogio;

    private bool mensagemMostrada = false;
    private bool podeAtivar = true;

    private void Start()
    {
        if (player != null)
            scriptMovimento = player.GetComponent<MonoBehaviour>();

        if (relogioUI != null)
            relogioUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !podeAtivar || mensagemMostrada) return;

        Debug.Log("Checkpoint do circuito alcançado.");
        StartCoroutine(ContagemComEsperaCoroutine());

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagemDoCapitao);
        }
        else
        {
            Debug.LogWarning("CapitanDialog NÃO encontrado no CheckpointCircuito.");
        }

        mensagemMostrada = true;
        podeAtivar = false;
        Invoke(nameof(ReativarCheckpoint), tempoCooldown);
    }

    private void ReativarCheckpoint()
    {
        podeAtivar = true;
    }

    private IEnumerator ContagemComEsperaCoroutine()
    {
        if (scriptMovimento != null)
            scriptMovimento.enabled = false;

        yield return new WaitForSeconds(5f); // Espera antes da contagem

        if (textoContagem != null)
        {
            textoContagem.gameObject.SetActive(true);
            textoContagem.text = "3";
            yield return new WaitForSeconds(1f);
            textoContagem.text = "2";
            yield return new WaitForSeconds(1f);
            textoContagem.text = "1";
            yield return new WaitForSeconds(1f);
            textoContagem.text = "Vai";
            yield return new WaitForSeconds(1f);
            textoContagem.gameObject.SetActive(false);
        }

        // Ativa relógio e som de tique-taque
        if (relogioUI != null)
            relogioUI.SetActive(true);
        if (somRelogio != null)
            somRelogio.Play();

        if (scriptMovimento != null)
            scriptMovimento.enabled = true;

        TutorialManager.Instance.IniciarContagem();
    }
}
