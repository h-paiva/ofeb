using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [Header("Controle do Movimento")] 
    [SerializeField] public float SpeedWalk;
    [SerializeField] public float SpeedRun;
    [Header("Controle do Pulo")] 
    [SerializeField] public float JumpForce;
    [SerializeField] public bool isJumping = false;

     // Sistema de Vida
    public int maxHealth = 100; // Vida máxima do player
    public int currentHealth; // Vida atual do player
    private Rigidbody2D rig;
    private Animator anim;
    private ArmControl armControl;
    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        currentHealth = maxHealth; // Inicializa a vida atual com a vida máxima
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
        if (collision.gameObject.CompareTag("EnemyBullet")) // Verifica se foi atingido por uma bala inimiga
        {
            TakeDamage(10); // Aplica dano ao player
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.layer == 7){
            isJumping = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Aplica dano à vida atual do player

        if (currentHealth <= 0)
        {
            Die(); // Se a vida atual for igual ou menor que zero, o player morre
        }
    }

    private void Die()
    {
        // Código para quando o player morre (por exemplo, reiniciar o jogo ou mostrar uma tela de game over)
        Debug.Log("Player died!");
        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
