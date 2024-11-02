using System.Threading;
using UnityEngine;

public class PainelControl : MonoBehaviour
{
    [SerializeField] private Transform panelSalaMaker;
    [SerializeField] private Transform panelCanal3Expo;
    [SerializeField] private Transform panelMainMenu;
    [SerializeField] private Transform panelOptions;
    [SerializeField] public float duration;

    // Função para alternar entre dois painéis
    public void Start()
    {
            //Invoke("salaMakerIntro", 0);
            //Invoke("canal3Expo", duration);
            //Invoke("mainMenu", duration*2);
    }
    void salaMakerIntro()
    {
        panelSalaMaker.gameObject.SetActive(true);
    }
    void canal3Expo()
    {
        panelSalaMaker.gameObject.SetActive(false);
        panelCanal3Expo.gameObject.SetActive(true);
    }
    void mainMenu()
    {
        panelCanal3Expo.gameObject.SetActive(false);
        panelMainMenu.gameObject.SetActive(true);
    }
}
