using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpaeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatCollisionBullet;
    private Rigidbody2D rig;

    private void Start() 
    {
        rig = GetComponent<Rigidbody2D>();
        SetStraighVelocity();
        SetDestroyBullet();
    }
    void Update(){
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, distance , whatCollisionBullet);
        if(hitInfo.collider != null){
            if(hitInfo.collider.CompareTag("Enemy")){
                //hitInfo.collider.GetComponent<EnemyAI>().TakeDamage(damage);
                Debug.Log("Acertou o inimigo");
                Destroy(gameObject);  
            }
            if(hitInfo.collider.CompareTag("Ground")){
                //hitInfo.collider.GetComponent<EnemyAI>().TakeDamage(damage);
                Debug.Log("Acertou o chao");
                Destroy(gameObject);  
            }
            
        }
    }
    private void SetStraighVelocity()
    {
        rig.velocity = transform.right * normalBulletSpaeed;
    }
    void SetDestroyBullet(){
        Destroy(gameObject, bulletLifeTime);
    }
}
