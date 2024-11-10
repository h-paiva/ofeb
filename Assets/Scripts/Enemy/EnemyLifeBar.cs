using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{
    public Slider lifeBar;
    [SerializeField] private float lifeBarFull = 100f; 

    public void DamageLife(float damage){
        lifeBarFull -= damage;
        lifeBar.value = lifeBarFull;
    }
}
