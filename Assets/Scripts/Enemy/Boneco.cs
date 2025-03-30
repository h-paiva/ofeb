using UnityEngine;

public class Personagem : MonoBehaviour
{
    // Método para lidar com a colisão com a bala
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet_Pixel")) // Verifica se o objeto colidido tem a tag bullet_Pixel
        {
            // Faz o personagem desaparecer
            gameObject.SetActive(false); // Desativa o objeto
            Destroy(other.gameObject); // Destroi a bala após atingir o personagem
        }
    }
}