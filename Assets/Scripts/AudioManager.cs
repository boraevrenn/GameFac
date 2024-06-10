using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] AudioSource audioSource;
    [Header("Audio Clips")]
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip walkClip;

    private void Update()
    {
        PlayerWalkSound();
    }

    void PlayerWalkSound()
    {
        if (playerRigidbody != null)
        {
            if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkClip);

            }
        }
    }
    public void PlaySwordSound()
    {
        audioSource.PlayOneShot(attackClip);    
    }
}
