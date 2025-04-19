using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float damage = 01f; // Dano da bala

    private Rigidbody2D rig;

    private void Awake()
    {
        damage = 1f; // Isso sobrescreve qualquer valor do Inspector.
    }

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

    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Colidiu com Espantalho
        if (collision.CompareTag("Scarecrow"))
        {
            Scarecrow scarecrow = collision.GetComponent<Scarecrow>();
            if (scarecrow != null)
            {
                scarecrow.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        // Colidiu com Caixa
        else if (collision.CompareTag("Box"))
        {
            Box box = collision.GetComponent<Box>();
            if (box != null)
            {
                box.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        // Colidiu com chão
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
