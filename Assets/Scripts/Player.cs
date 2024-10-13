using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool isJumping = false;


    private Rigidbody2D rig;
    private Animator anim;
    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis( "Horizontal"), 0f , 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if(Input.GetAxis( "Horizontal") > 0f){
            if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("walk", true);
            }
            
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }else if(Input.GetAxis( "Horizontal") < 0f){
            if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("walk", true);
            }
        }else{
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
    }
}
