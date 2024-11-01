using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private float distance = 0.1f;
    [SerializeField] private LayerMask whatCollisionBullet;
    [SerializeField] private int damage = 10;

    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, bulletLifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatCollisionBullet);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("Acertou o inimigo");
                hitInfo.collider.GetComponent<EnemyUnified>().TakeDamage(damage);
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

