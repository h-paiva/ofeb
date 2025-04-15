using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float damage = 10f; // Dano causado pela bala

    private Rigidbody2D rig;

    private void Start() 
    {
        rig = GetComponent<Rigidbody2D>();
        SetStraightVelocity();
        SetDestroyBullet();
    }

    private void SetStraightVelocity()
    {
        rig.velocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroyBullet()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    public float GetDamage() // Método que retorna o dano da bala
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scarecrow"))
        {
            collision.GetComponent<Scarecrow>().TakeDamage(1f);
            Destroy(gameObject); // Destroi a bala ao colidir
        }
    }
}

