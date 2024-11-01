using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private float distance = 0.1f;
    [SerializeField] private LayerMask whatCollisionBullet;
    [SerializeField] private int damage = 5;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        if (rb != null)
        {
            direction = dir.normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }

    void Update()
    {
        if (direction != Vector2.zero)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, distance, whatCollisionBullet);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Debug.Log("Acertou o jogador");
                    // hitInfo.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                else if (hitInfo.collider.CompareTag("Ground"))
                {
                    Debug.Log("Acertou o chão");
                    Destroy(gameObject);
                }
            }
        }
    }
}