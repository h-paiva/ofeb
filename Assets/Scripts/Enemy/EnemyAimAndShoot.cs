using UnityEngine;

public class EnemyAimAndShoot : MonoBehaviour
{
    private Vector2 direction;
    private float angle;
    private GameObject bulletInst;
    private Transform playerTransform; // Referência ao transform do jogador

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private float shootingRange = 10f; // Alcance máximo para atirar
    [SerializeField] private float timeBetweenShots = 2f; // Tempo entre os tiros
    private float nextTimeToFire = 0f;
    public static bool isFrozen = false; //Freeza o braço inimigo

    void Start()
    {
        // Encontrar o jogador na cena
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isFrozen) return;
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= shootingRange) // Só mira e atira se o jogador estiver no alcance
            {
                HandlerGunRotation();
                HandlerGunShooting();
            }
        }
    }

    private void HandlerGunRotation()
    {
        // Rotaciona a arma em direção ao jogador
        direction = ((Vector2)playerTransform.position - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        // Flip da arma baseado no ângulo
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if(angle > 90 || angle < -90)
        {
            localScale.y = -1f;
            body.flipX = true;
        }
        else
        {
            localScale.y = 1f;
            body.flipX = false;
        }
        gun.transform.localScale = localScale;
    }

    private void HandlerGunShooting()
    {
        // Atira em intervalos regulares
        if (Time.time >= nextTimeToFire)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
            nextTimeToFire = Time.time + timeBetweenShots;
        }
    }

    // Opcional: Para visualizar o alcance no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}