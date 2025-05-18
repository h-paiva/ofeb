using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatCollisionBullet;
    [SerializeField] private float damage = 10f; // Dano causado pela bala

    private Rigidbody2D rig;

    private void Start() 
    {
        rig = GetComponent<Rigidbody2D>();
        SetStraightVelocity();
        SetDestroyBullet();
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatCollisionBullet);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamage(10f);
                }
                Debug.Log("Acertou o inimigo");
                Destroy(gameObject);
            }
            else if(hitInfo.collider.CompareTag("Player"))
            {
                Player player = hitInfo.collider.GetComponent<Player>();
                if(player != null)
                {
                    player.TakeDamage(damage);
                }
                Debug.Log("Acertou o jogador");
                Destroy(gameObject);
            }
            else if(hitInfo.collider.CompareTag("Ground"))
            {
                Debug.Log("Acertou o chão");
                Destroy(gameObject);
            }
        }
    }

    public void SetStraightVelocity()
    {
        rig.velocity = transform.right * normalBulletSpeed;
    }

    void SetDestroyBullet()
    {
        Destroy(gameObject, bulletLifeTime);
    }
}