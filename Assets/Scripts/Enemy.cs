using UnityEngine;

public class EnemyUnified : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject arm;
    [SerializeField] private Transform firePoint;
    public Transform player;

    [Header("Configurações de Combate")]
    public float shootingRange = 3f;
    public float fireRate = 1f;
    public int health = 50;
    public GameObject bulletPrefab;

    [Header("Configurações do Braço")]
    [SerializeField] private Transform armPositionIdleWalk;
    [SerializeField] private Transform armPositionRunRight;
    [SerializeField] private Transform armPositionRunLeft;

    private float nextFireTime = 0f;
    private bool isFacingRight = true;

    void Start()
    {
        if (arm == null)
            arm = gameObject;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        HandleDirection();
        CheckAndShoot();
    }

    void HandleDirection()
    {
        // Controle de direção do inimigo
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }

        // Atualiza posição do braço
        arm.transform.position = isFacingRight ? armPositionRunRight.position : armPositionRunLeft.position;
    }

    void CheckAndShoot()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (player != null && firePoint != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
             enemyBullet.SetDirection(direction);
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}