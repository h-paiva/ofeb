using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReload : MonoBehaviour
{
    public int bullets = 8;
    public float reloadTime = 2f;
    public float fireRate = 1.5f;
    public Text bulletsText;

    public static PlayerReload playerReload;
    // Start is called before the first frame update
    void Awake()
    {
        //PERSISTENCIA NAS CENAS
        if (playerReload == null)
        {
            playerReload = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateBulletsUI(bullets);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBulletsUI(int bullets)
    {
        bulletsText.text = bullets.ToString();
    }
}
