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

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

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
        if (player != null)
            audioSource.PlayOneShot(attackClip);
    }
}
