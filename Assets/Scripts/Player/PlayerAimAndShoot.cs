
using System.Collections;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    private GameObject bulletInst;
    public CameraConfig cameraConfig;
    public static bool isFrozen = false; //Freeza o braço

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SpriteRenderer body;

    //VARIAVEIS PARA O RELOAD

    PlayerReload playerReload;
    private int bullets;
    private float reloadTime;
    public float fireRate;
    public bool reloading;


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
        UpdateBulletsUI();
    }

    private void HandlerGunRotation()
    {
        if (isFrozen) return;
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
        if (Input.GetMouseButtonDown(0) && bullets > 0)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

            Rigidbody2D rb = bulletInst.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(gun.transform.right * bulletForce, ForceMode2D.Impulse);
            }

            bullets--;
            UpdateBulletsUI();
        }
        if (Input.GetKeyDown(KeyCode.R) && !reloading && bullets < playerReload.bullets)
        {
            StartCoroutine(Reloading());
        }
        else if (Input.GetMouseButtonDown(0) && bullets <= 0 && !reloading)
        {
            StartCoroutine(Reloading());
        }

    }

    public void SetReloadStatus()
    {
        fireRate = playerReload.fireRate;
        bullets = playerReload.bullets;
        reloadTime = playerReload.reloadTime;
    }

    public void UpdateBulletsUI()
    {
        FindObjectOfType<PlayerReload>().UpdateBulletsUI(bullets);
    }

    IEnumerator Reloading()
    {
        reloading = true;
        //anim.SetBool("Reloading", true); // Caso ponha uma Animação de reloading
        yield return new WaitForSeconds(reloadTime);
        bullets = playerReload.bullets;
        reloading = false;
        //anim.SetBool("Reloading", false); // Caso ponha uma Animação de reloading
        UpdateBulletsUI();
    }
}
