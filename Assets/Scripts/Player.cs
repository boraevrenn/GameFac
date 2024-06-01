using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;


    [Header("Adjustable Values")]
    [SerializeField] float moveSpeed = 500;
    [SerializeField] float jumpSpeed = 500;


    float rotationValue = 1;


    public void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, playerRigidbody.velocity.y);
        playerRigidbody.velocity = move;
    }

    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 move = new Vector2(playerRigidbody.velocity.x, jumpSpeed * Time.deltaTime);
            playerRigidbody.velocity = move;
        }
    }

    public void Rotate()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
        {
            if (playerRigidbody.velocity.x < -rotationValue)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
            else if (playerRigidbody.velocity.x > rotationValue)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
        }
    }

    public void MoveAnimation()
    {

    }
}

