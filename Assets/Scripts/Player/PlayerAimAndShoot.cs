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
    public CameraConfig cameraConfig;

    [SerializeField] private float bulletForce = 10f;

    private void Start() 
    {
        cameraConfig = FindObjectOfType<CameraConfig>();

        if (gun == null || bullet == null || bulletSpawnPoint == null)
        {
            Debug.LogWarning("⚠️ Variáveis obrigatórias ('gun', 'bullet' ou 'bulletSpawnPoint') não estão atribuídas no PlayerAimAndShoot!");
        }
    }

    void Update()
    {   
        HandlerGunRotation(); 
        HandlerGunShooting();
    }

    private void HandlerGunRotation()
    {
        // rotate the gun towards the mouse position
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        // flip the gun when it reaches a 90 degree threshold
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
        if (Input.GetMouseButtonDown(0))
        {
            if (bullet != null && bulletSpawnPoint != null && gun != null)
            {
                // Verifica se o spawn ainda está ativo
                if (!bulletSpawnPoint.gameObject.activeInHierarchy)
                {
                    Debug.LogWarning("⚠️ bulletSpawnPoint está desativado ou foi destruído!");
                    return;
                }

                bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

                Rigidbody2D rb = bulletInst.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(gun.transform.right * bulletForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                Debug.LogWarning("⚠️ Tentando atirar, mas 'bullet', 'bulletSpawnPoint' ou 'gun' está faltando!");
            }
        }
    }

}