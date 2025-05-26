using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacter : MonoBehaviour
{
    [SerializeField] AudioSource footstepsAudioSource = null;
    
    [Header("Audio clips")]
    [SerializeField] AudioSource softGround = null;
    [SerializeField] AudioSource hardGround = null;

    [Header("Steps")]
    [SerializeField] float timer = 0.5f;

    private float stepsTimer;

    public void PlaySteps(Player.GroundType groundType, float speedNormalized)
    {
        if (groundType == Player.GroundType.none)
            return;

        stepsTimer += Time.fixedDeltaTime * speedNormalized;

        if (stepsTimer >= timer)
        {
            var steps = groundType == Player.GroundType.hardGround ? softGround : hardGround;
            //int index = Random.Range(0, steps.Length);
            //footstepsAudioSource.PlayOneShot(steps[index]);

            stepsTimer = 0;
        }
    }
}
