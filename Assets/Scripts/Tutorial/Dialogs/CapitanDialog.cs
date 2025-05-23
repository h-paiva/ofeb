using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CapitanDialog : MonoBehaviour
{
    public string[] dialogueTut;
    public int dialogueIndex;

    public GameObject mensagesPanel;
    public Text dialogueText;

    public Text nameCapitan;
    public Image imageCapitan;
    public Sprite spriteCapitan;

    public float tempoDeExibicao = 5f;

    private Coroutine mensagemCorrente;

    void Start()
    {
        mensagemCorrente = StartCoroutine(StartDialogueAutomaticamente());
    }

    IEnumerator StartDialogueAutomaticamente()
    {
        if (mensagesPanel != null && nameCapitan != null && imageCapitan != null && dialogueText != null)
        {
            mensagesPanel.SetActive(true);
            nameCapitan.text = "Paiva";
            imageCapitan.sprite = spriteCapitan;

            dialogueIndex = 0;

            if (dialogueTut.Length > 0)
            {
                dialogueText.text = dialogueTut[dialogueIndex];
            }

            Debug.Log("Mostrando mensagem inicial: " + dialogueText.text);

            yield return new WaitForSeconds(tempoDeExibicao);
            mensagesPanel.SetActive(false);
            mensagemCorrente = null;
        }
        else
        {
            Debug.LogWarning("CapitanDialog: Referências não atribuídas corretamente no Inspector.");
        }
    }

    public void MostrarMensagemTemporaria(string mensagem)
    {
        Debug.Log("MostrarMensagemTemporaria chamada com: " + mensagem);

        if (mensagesPanel != null)
            mensagesPanel.SetActive(true); // <-- FORÇA O CANVAS ATIVAR IMEDIATAMENTE

        if (mensagemCorrente != null)
        {
            StopCoroutine(mensagemCorrente);
            Debug.Log("Coroutine anterior parada.");
        }

        mensagemCorrente = StartCoroutine(ExibirMensagemTemporaria(mensagem));
    }

    private IEnumerator ExibirMensagemTemporaria(string mensagem)
    {
        if (mensagesPanel != null && nameCapitan != null && imageCapitan != null && dialogueText != null)
        {
            nameCapitan.text = "Capitão Paiva";
            imageCapitan.sprite = spriteCapitan;
            dialogueText.text = mensagem;
            mensagesPanel.SetActive(true);

            Debug.Log("Exibindo mensagem temporária: " + mensagem);

            yield return new WaitForSeconds(tempoDeExibicao);

            mensagesPanel.SetActive(false);
            mensagemCorrente = null;
        }
    }
}
