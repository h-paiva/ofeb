using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatCollisionBullet;
    [SerializeField] private float damage = 1f; // Dano configurável da bala no Inspector

    private Rigidbody2D rig;

    private void Start() 
    {
        rig = GetComponent<Rigidbody2D>();
        SetStraightVelocity(); // Define a velocidade inicial da bala
        SetDestroyBullet();    // Define o tempo de vida da bala
    }

    void Update()
    {
        // Cria um raio na frente da bala para detectar colisões com base na LayerMask
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatCollisionBullet);
        //Testando para funcionar a caixa
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, rig.velocity.normalized, distance, whatCollisionBullet);
        //Não funcionou
        Debug.DrawRay(transform.position, rig.velocity.normalized * distance, Color.red);



        if (hitInfo.collider != null)
        {
            // Acertou inimigo
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); // CORREÇÃO: usar o valor configurado no Inspector
                }
                Debug.Log("Acertou o inimigo");
                Destroy(gameObject);
            }

            // Acertou jogador
            else if (hitInfo.collider.CompareTag("Player"))
            {
                Player player = hitInfo.collider.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
                Debug.Log("Acertou o jogador");
                Destroy(gameObject);
            }

            // Acertou chão
            else if (hitInfo.collider.CompareTag("Ground"))
            {
                Debug.Log("Acertou o chão");
                Destroy(gameObject);
            }

            // Acertou caixa
            else if (hitInfo.collider.CompareTag("Box"))
            {
                Box box = hitInfo.collider.GetComponent<Box>();
                if (box != null)
                {
                    box.TakeDamage(damage);
                    Debug.Log("Script Box encontrado, aplicando dano..."); //detectar se o script está funcionando com a caixa

                }
                //detectar se o script não está funcionando com a caixa
                else
                {
                    Debug.LogWarning("Objeto com tag 'Box' não tem o script Box.cs.");
                }

                Debug.Log("A bala detectou colisão com objeto: " + hitInfo.collider.name);
                Destroy(gameObject);
            }
        }

        // Mostra o raio no editor para fins de debug
        Debug.DrawRay(transform.position, transform.right * distance, Color.red);
    }

    // Aplica a velocidade inicial para frente
    public void SetStraightVelocity()
    {
        rig.velocity = transform.right * normalBulletSpeed;
    }

    // Destroi a bala após certo tempo, caso não colida com nada
    void SetDestroyBullet()
    {
        Destroy(gameObject, bulletLifeTime);
    }
}
