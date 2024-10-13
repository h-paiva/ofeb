using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    [SerializeField] private Transform armPositionIdleWalk;
    [SerializeField] private Transform armPositionRunRight;
    [SerializeField] private Transform armPositionRunLeft;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    void Start()
    {
        transform.position = armPositionIdleWalk.position;
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        direction = (worldPosition - (Vector2)arm.transform.position).normalized;

        //flip the gun when itreaches a 90 degree threshold
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Horizontal") != 0f){
            if(angle >= -90f && angle <= 90f)
            {
                transform.position = armPositionRunRight.position;
            }
            else if(angle > 90f || angle < -90f)
            {
                transform.position = armPositionRunLeft.position;
            }
        }else
        {

            transform.position = armPositionIdleWalk.position;
        }
    }
}
