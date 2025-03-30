using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionControll : MonoBehaviour
{
    private Vector2 direction;
    private Transform playerTransform;
    private float nextTimeToFire = 0f;
    private GameObject bulletInst;
    [SerializeField] private GameObject body;
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private Transform arm;
    [SerializeField] private SpriteRenderer armSprite;
    [SerializeField] private Transform armPositionRight;
    [SerializeField] private Transform armPositionLeft;
    [SerializeField] private float shootingRange = 10f; 
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float timeBetweenShots = 2f;

    private Animator animator; // Para controlar o Animator
    // Start is called before the first frame update
    void Start()
    {
        arm.position = armPositionRight.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayper();
    }
    private void FindPlayper()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            
            if (distanceToPlayer <= shootingRange) // Só mira e atira se o jogador estiver no alcance
            {
                ChangePositionToPlayer(); 
                HandlerGunShooting();
            }
        }
    }
    private void ChangePositionToPlayer()
    {
        direction = ((Vector2)playerTransform.position - (Vector2)body.transform.position).normalized;
        arm.transform.right = direction;

        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if(direction.x < 0)
        {
            bodySprite.flipX = true;
            arm.position = armPositionLeft.position;
            localScale.y = -1f;
        }
        else
        {
            bodySprite.flipX = false;
            arm.position = armPositionRight.position;
            localScale.y = 1f;
        }
        arm.transform.localScale = localScale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
    private void HandlerGunShooting()
    {
        // Atira em intervalos regulares
        if (Time.time >= nextTimeToFire)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, arm.transform.rotation);
            nextTimeToFire = Time.time + timeBetweenShots;

        }
    }
}
