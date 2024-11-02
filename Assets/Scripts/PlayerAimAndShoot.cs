using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    private GameObject bulletInst;

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SpriteRenderer body;

    void Update()
    {   
        HandlerGunRotation(); 
        HandlerGunShooting();
    }

    private void HandlerGunRotation()
    {
        //rotate the gun towrds the mouse position
        worldPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        //flip the gun when itreaches a 90 degree threshold
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if(angle > 90 || angle < -90)
        {
            localScale.y = -1f;
            body.flipX = true;
        }
        else
        {
            localScale.y = 1f;
            body.flipX = false;
        }
        gun.transform.localScale = localScale;

    }

    private void HandlerGunShooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }

    }
}
