using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointFinal : MonoBehaviour
{
    [Header("Mensagem do Capitão")]
    [TextArea]
    [SerializeField] private string mensagemFinal = "Parabens ''Axpira'', nao foi tao ruim quanto pensei, mas ainda precisamos vencer as nossas missões. Vamos que irei te orientar sobre elas.";

    private bool jaFinalizou = false; // Garante que o checkpoint só será ativado uma vez

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (jaFinalizou || !collision.CompareTag("Player")) return;

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.PararContagem();
            jaFinalizou = true;

            Player.isFrozen = true;
            PlayerAimAndShoot.isFrozen = true;

            Animator anim = collision.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                anim.SetBool("jump", false);
                anim.SetBool("idle", true);
            }

            Invoke("CarregarProximaCena", 3f);
        }

        print("Checkpoint final alcançado!");

        CapitanDialog capitanDialog = FindObjectOfType<CapitanDialog>();
        if (capitanDialog != null)
        {
            capitanDialog.MostrarMensagemTemporaria(mensagemFinal);
        }
    }

    private void CarregarProximaCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
