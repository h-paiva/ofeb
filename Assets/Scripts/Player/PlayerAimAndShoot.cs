using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    // Variáveis internas
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    // Referências públicas/serializadas
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SpriteRenderer body;
    public CameraConfig cameraConfig;

    [SerializeField] private float bulletForce = 10f;

    private void Start()
    {
        // Busca automática de referências se necessário
        if (cameraConfig == null)
            cameraConfig = FindObjectOfType<CameraConfig>();

        // Validações
        if (gun == null || bullet == null || bulletSpawnPoint == null || body == null)
            Debug.LogWarning("⚠️ Algumas referências não foram atribuídas no Inspector!");
    }

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        if (gun == null || Camera.main == null || body == null)
            return;

        // Pega a posição do mouse no mundo
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;

        // Calcula o ângulo para rotação
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica a rotação diretamente no eixo Z
        gun.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Reflete personagem e braço baseado na direção do mouse
        if (angle > 90 || angle < -90)
        {
            body.flipX = true;
            gun.transform.localScale = new Vector3(1, -1, 1); // Inverte o braço
            cameraConfig?.CameraFollowPlayer("left");
        }
        else
        {
            body.flipX = false;
            gun.transform.localScale = new Vector3(1, 1, 1);
            cameraConfig?.CameraFollowPlayer("right");
        }
    }

    private void HandleGunShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bullet != null && bulletSpawnPoint != null && gun != null)
            {
                GameObject bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

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
