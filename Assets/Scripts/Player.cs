using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;


    [Header("Adjustable Values")]
    [SerializeField] float moveSpeed = 500;
    [SerializeField] float jumpSpeed = 500;



    float rotationChangeValue = 1;



    void Update()
    {
        PlayAnimations();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }


    public void PlayerMovement()
    {
        Move();
        Jump();
        Rotate();
    }
    public void PlayAnimations()
    {
        MoveAnimation();
        JumpAnimation();
    }



    //Player move methods
    void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, playerRigidbody.velocity.y);
        playerRigidbody.velocity = move;
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jump = new Vector2(playerRigidbody.velocity.x, jumpSpeed * Time.deltaTime);
            playerRigidbody.velocity = jump;
        }
    }

    void Rotate()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
        {
            if (playerRigidbody.velocity.x < -rotationChangeValue)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (playerRigidbody.velocity.x > rotationChangeValue)
                transform.localScale = new Vector3(1, 1, 1);
        }
    }


    //Player Animation Methods
    void MoveAnimation()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
            playerAnimator.SetBool("isRunning", true);
        else
            playerAnimator.SetBool("isRunning", false);
    }

    void JumpAnimation()
    {

        if (!playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
            playerAnimator.SetBool("isJump", true);
        else if (playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
            playerAnimator.SetBool("isJump", false);
    }
}


