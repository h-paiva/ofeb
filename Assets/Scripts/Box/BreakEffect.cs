using UnityEngine;

public class BreakEffect : MonoBehaviour
{
    [SerializeField] private float destroyTime = 20f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
