using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public ImgMenu imgmenu;
    public GameObject menuOpcoes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(imgmenu.isPlaying && Input.anyKeyDown)
        {
            imgmenu.Play();
            menuOpcoes.SetActive(true);
        }
    }
}
