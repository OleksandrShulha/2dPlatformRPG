using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayrMove : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D capsuleCollider2;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2 = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();

        if (!capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }



    }

    void  OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJunp(InputValue value)
    {
        if (!capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;           
        }
        if (value.isPressed)
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playrVelocity = new Vector2(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = playrVelocity;
        bool playrAsHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        if (playrAsHorizontalSpeed)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    void FlipSprite()
    {
        bool playrAsHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playrAsHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        else
            transform.localScale = new Vector2(1f, 1f);
    }
}
