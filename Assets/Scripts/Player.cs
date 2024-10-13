using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Controle do Movimento")] 
    [SerializeField] public float SpeedWalk;
    [SerializeField] public float SpeedRun;
    [Header("Controle do Pulo")] 
    [SerializeField] public float JumpForce;
    [SerializeField] public bool isJumping = false;
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
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis( "Horizontal"), 0f , 0f);
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("run", false);
            transform.position += movement * Time.deltaTime * SpeedWalk;
        }else{
            transform.position += movement * Time.deltaTime * SpeedRun;
        }
        if(Input.GetAxis( "Horizontal")!= 0f)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);
            }
            else if(!Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("walk", true);
            }
        }
        if(Input.GetAxis( "Horizontal") > 0f)
        {
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }else if(Input.GetAxis( "Horizontal") < 0f)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);
            }
            else if(!Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("walk", true);
            }
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 7){
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.layer == 7){
            isJumping = true;
        }
    }
}
