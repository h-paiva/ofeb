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
    [SerializeField] public Transform armWalk;
    [SerializeField] public GameObject armShoot;
    public PlayerLifeBar playerLifeBar;
    private Rigidbody2D rig;
    private Animator anim;

    [Header("Status do Jogador")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [SerializeField] private float cameraMinX = -50f;
    [SerializeField] private float cameraMaxX = 50f;
    public ArmControl armControl;

    //Para congelar o player no final do Tutorial - caso encontre outra utilidade pode usar tambem
    public static bool isFrozen = false;

    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        currentHealth = maxHealth;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (isFrozen) return;

        Move();
        Jump();
    }

    void Move()
    {
        if (isFrozen) return;
        
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
        
        float clampedX = Mathf.Clamp(transform.position.x, cameraMinX, cameraMaxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void Jump()
    {
        if (isFrozen) return;
        
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
            armControl.PlayerIsJumping(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Permite pulo quando encosta no chão (Layer 7) ou em caixas com tag "Box"
        if(collision.gameObject.layer == 7 || collision.gameObject.CompareTag("Box")){
            // Verifica se a colisão veio de baixo para cima (ou seja, o player está pisando)
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isJumping = false;
                    anim.SetBool("jump", false);
                    armControl.PlayerIsJumping(false);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // Volta a considerar que o player está pulando ao sair do chão ou da caixa
        if(collision.gameObject.layer == 7 || collision.gameObject.CompareTag("Box")){
            // Usa uma verificação segura com delay para checar se ainda está no chão
            StartCoroutine(VerificarSeAindaEstaNoChao());
        }
    }

    // Corrotina que aguarda um frame para verificar colisões e garantir se o player saiu realmente do chão
    private IEnumerator VerificarSeAindaEstaNoChao()
    {
        yield return new WaitForFixedUpdate(); // Espera o próximo frame de física

        ContactPoint2D[] contatos = new ContactPoint2D[10];
        int quantidade = rig.GetContacts(contatos);

        bool estaNoChao = false;

        for (int i = 0; i < quantidade; i++)
        {
            if (contatos[i].normal.y > 0.5f)
            {
                estaNoChao = true;
                break;
            }
        }

        isJumping = !estaNoChao;
    }

    // Método para receber dano
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        playerLifeBar.DamageLife(damage);
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    // Método quando o jogador morre
    private void Die()
    {
        // Opcional: Tocar animação de morte
        // anim.SetTrigger("die");
        
        // Implementar lógica de morte (game over, respawn, etc)
        armShoot.gameObject.SetActive(false);
        anim.SetBool("dead", true);
        Debug.Log("Player Died");
        
        // Opcional: Desativar o jogador
        // gameObject.SetActive(false);
    }
}
