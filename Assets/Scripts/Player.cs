using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Attack attack;


    [Header("Adjustable Values")]
    [SerializeField] float moveSpeed = 500;
    [SerializeField] float jumpSpeed = 500;


    [Header("Animation Values")]


    float rotationChangeValue = 1;
    float rotationValue = 180;





    public void PlayerPlay()
    {
        PlayerMovement();
        PlayAnimations();
        attack.ApplyAttack();
    }
    void PlayerMovement()
    {
        Move();
        Jump();
        Rotate();
    }
    void PlayAnimations()
    {
        MoveAnimation();
        JumpAnimation();
        SwordAnimation();
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
                transform.rotation = Quaternion.Euler(transform.rotation.x, rotationValue, transform.rotation.z);
            else if (playerRigidbody.velocity.x > rotationChangeValue)
                transform.rotation = Quaternion.Euler(transform.rotation.x, rotationValue - rotationValue, transform.rotation.z);
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

    void SwordAnimation()
    {
        if (Input.GetMouseButton(0) && attack.attackTimer >= attack.attackTimerTotal)
        {
            playerAnimator.SetTrigger("Attack");
        }
    }
}


