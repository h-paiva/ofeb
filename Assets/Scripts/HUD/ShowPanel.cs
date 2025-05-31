using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject panel; // Assign the panel GameObject in the Inspector

    public void ShowPanelAction()
    {
        Debug.LogWarning("acionou o ShowPanelAction.");
        panel.SetActive(true);
    }
}
