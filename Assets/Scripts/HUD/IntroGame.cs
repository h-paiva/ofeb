using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGame : MonoBehaviour
{
    [SerializeField] public GameObject FatecImage; 
    [SerializeField] public GameObject StudioImage; 
    void Start()
    {
        Invoke("FatecLogo", 3);
        Invoke("StudioLogo", 6);
    }
    void FatecLogo()
    {
        FatecImage.gameObject.SetActive(false);
    }
    void StudioLogo()
    {
        StudioImage.gameObject.SetActive(false);
    }
}
