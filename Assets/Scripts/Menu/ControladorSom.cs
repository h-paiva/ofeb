using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSom : MonoBehaviour
{
    [SerializeField] private AudioSource musica;

    public void VolumeMusical(float value)
    {
        musica.volume = value;
    }
}