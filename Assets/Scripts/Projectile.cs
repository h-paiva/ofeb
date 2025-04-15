using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public int lifeTime;
    public float distance;
    public LayerMask layerEnemy;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, layerEnemy);
        if (hitInfo.collider != null)
        {
            var enemy = hitInfo.collider.GetComponent<MonoBehaviour>();
            if (enemy != null)
            {
                var method = enemy.GetType().GetMethod("TakeDamage");
                if (method != null)
                {
                    method.Invoke(enemy, new object[] { damage });
                }
            }

            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
