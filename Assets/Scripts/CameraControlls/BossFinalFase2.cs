using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinalFase2 : MonoBehaviour
{
    public Transform jogador;
    public CameraConfig cameraConfig;
    public Player player;

    private bool ativado = false;

    void Update()
    {
        if (!ativado && jogador.position.x > transform.position.x)
        {
            ativado = true;
            cameraConfig.SetBossFinalFase2(true);
            player.SetBossFinalFase2(true);
        }
    }
}
