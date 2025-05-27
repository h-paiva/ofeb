using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Controle do Movimento")]
    [SerializeField] public float SpeedWalk;
    [SerializeField] public float SpeedRun;
    [Header("Controle do Movimento")]
    [SerializeField] public float faseMin;
    [SerializeField] public float faseMax;
    [SerializeField] public float faseMinBoss;
    [SerializeField] public float faseMaxBoss;
    [Header("Controle do Pulo")]
    [SerializeField] public float JumpForce;
    [SerializeField] public bool isJumping = false;
    [SerializeField] public Transform armWalk;
    [SerializeField] public GameObject armShoot;
    [SerializeField] private Transform restartMenu;
    public PlayerLifeBar playerLifeBar;
    private Rigidbody2D rig;
    private Animator anim;

    [Header("Status do Jogador")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public ArmControl armControl;
    public float speed = 5f;

    [Header("Referência da Câmera")]
    public Transform cameraTransform;

    private bool bossFinalFase2 = false;
    //Para congelar o player no final do Tutorial - caso encontre outra utilidade pode usar tambem
    public static bool isFrozen = false;

    [Header("Som de Passos")]
    public AudioClip stepSoftClip;
    public AudioClip stepHardClip;
    private AudioSource audioSource;
    public float stepInterval = 0.5f; // intervalo entre sons de passos
    private float stepTimer;
    private GroundType currentGround = GroundType.None;


    public enum GroundType
    {
        None,
        Soft,
        Hard
    }


    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        currentHealth = maxHealth;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isFrozen) return;
        Move();
        Jump();
        CheckGroundType();
    }

    void Move()
    {
        if (isFrozen) return;

        // Variaveis de movimetação do player, -1 para esquerda, 0 parado ou 1 para direita
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(moveInput, 0f, 0f);

        // Variaveis para fazer o controle do personagem
        float positionPlayerNow = transform.position.x;
        float positionPlayerByCameraMin = cameraTransform.position.x + (bossFinalFase2 ? faseMinBoss : faseMin);
        float positionPlayerByCameraMax = cameraTransform.position.x + (bossFinalFase2 ? faseMaxBoss : faseMax);
        // Verifica se o jogador está tentando andar para a esquerda ou direita da camera
        if ((positionPlayerNow <= positionPlayerByCameraMin && moveInput < 0) || (positionPlayerNow >= positionPlayerByCameraMax && moveInput > 0))
        {
            movement.x = 0f;
        }
        else
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", false);
                transform.position += movement * Time.deltaTime * SpeedWalk;
            }
            else
            {
                transform.position += movement * Time.deltaTime * SpeedRun;
            }
            if (Input.GetAxis("Horizontal") != 0f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("run", true);
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("walk", true);
                }
            }
            if (Input.GetAxis("Horizontal") > 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("run", true);
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
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

        if (movement.x != 0)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayStepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void Jump()
    {
        if (isFrozen) return;

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
            armControl.PlayerIsJumping(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isJumping = false;
            anim.SetBool("jump", false);
            armControl.PlayerIsJumping(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isJumping = true;
        }
    }

    // Método para receber dano
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        playerLifeBar.DamageLife(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método quando o jogador morre
    private void Die()
    {
        // Implementar lógica de morte (game over, respawn, etc)
        armShoot.gameObject.SetActive(false);
        anim.SetBool("dead", true);
        Destroy(gameObject, 5f);
        restartMenu.gameObject.SetActive(true);
    }
    // Método para permitir que o jogador se mova mais quando estiver lutando contra o boss final da fase 2
    public void SetBossFinalFase2(bool finalBossActive)
    {
        bossFinalFase2 = finalBossActive;
    }

    void CheckGroundType()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            GroundIdentifier ground = hit.collider.GetComponent<GroundIdentifier>();
            if (ground != null)
            {
                currentGround = ground.groundType;
            }
        }
        else
        {
            currentGround = GroundType.None;
        }
    }
    
    void PlayStepSound()
    {
        switch (currentGround)
        {
            case GroundType.Soft:
                audioSource.PlayOneShot(stepSoftClip);
                break;
            case GroundType.Hard:
                audioSource.PlayOneShot(stepHardClip);
                break;
            default:
                break;
        }
    }
}
