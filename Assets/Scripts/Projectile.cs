using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public int lifeTime;
    public float distance;
    public LayerMask layerEnemy;


    // Start is called before the first frame update

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, distance, layerEnemy);
        if(hitInfo.collider != null){
            if(hitInfo.collider.CompareTag("Enemy")){
                //hitInfo.collider.GetComponent<EnemyAI>().TakeDamage(damage);
                
            }
            DestroyProjectile();
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
