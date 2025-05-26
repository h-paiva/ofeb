
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    private GameObject bulletInst;
    public CameraConfig cameraConfig;
    public static bool isFrozen = false;

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SpriteRenderer body;

    //VARIAVEIS PARA O RELOAD

    PlayerReload playerReload;
    private int bullets;
    private float reloadTime;
    public float fireRate;


    [SerializeField] private float bulletForce = 10f;

    private void Start()
    {
        cameraConfig = FindObjectOfType<CameraConfig>();
        playerReload = PlayerReload.playerReload;
        SetReloadStatus();
    }

    void Update()
    {
        if (isFrozen) return;
        HandlerGunRotation();
        HandlerGunShooting();
    }

    private void HandlerGunRotation()
    {
        if (PlayerAimAndShoot.isFrozen) return;
        //rotate the gun towrds the mouse position
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        //flip the gun when itreaches a 90 degree threshold
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
            body.flipX = true;
            cameraConfig.CameraFollowPlayer("left");
        }
        else
        {
            localScale.y = 1f;
            body.flipX = false;
            cameraConfig.CameraFollowPlayer("right");
        }
        gun.transform.localScale = localScale;

    }

    private void HandlerGunShooting()
    {
        if (isFrozen) return;
        if (Input.GetMouseButtonDown(0))
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

            Rigidbody2D rb = bulletInst.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(gun.transform.right * bulletForce, ForceMode2D.Impulse);
            }

            bullets--;
        }

    }

    public void SetReloadStatus()
    {
        fireRate = playerReload.fireRate;
        bullets = playerReload.bullets;
        reloadTime = playerReload.reloadTime;
    }

    void UpdateBulletsUI()
    {
        
    }
}
