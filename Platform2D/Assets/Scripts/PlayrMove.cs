using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayrMove : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climpSpeed = 3f;
    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D capsuleCollider2;

    float speedAnimation;
    bool isAlive = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2 = GetComponent<CapsuleCollider2D>();
        speedAnimation = animator.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimpLeader();
        AnimationHero();
        Die();
    }

    void  OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void ClimpLeader()
    {
        if (!capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Climp")))
        {
            rb2d.gravityScale = 2f;
            return;
        }

        Vector2 climpVelocity = new Vector2(rb2d.velocity.x, moveInput.y * climpSpeed);
        rb2d.velocity = climpVelocity;
        rb2d.gravityScale = 0f;

    }

    void OnJunp(InputValue value)
    {
        if (!isAlive) { return; }
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
    }

    void FlipSprite()
    {
        bool playrAsHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playrAsHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        else
            transform.localScale = new Vector2(1f, 1f);
    }


    void AnimationHero()
    {
        //швидкість по вертикалі
        bool playrAsVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;
        //швидкість по горизонталі
        bool playrAsHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        if (isAlive)
        {
            if (!capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
                capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Climp")) && playrAsVerticalSpeed)
            {
                animator.SetBool("isClimp", true);
                animator.speed = speedAnimation;
            }
            else if (!capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
                capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Climp")) && !playrAsVerticalSpeed)
            {
                animator.speed = 0f;
            }
            else
            {
                animator.SetBool("isClimp", false);
                animator.speed = speedAnimation;
            }

            if (!capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
                !capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Climp")))
            {
                animator.SetBool("isJump", true);
            }
            else
            {
                animator.SetBool("isJump", false);
            }

            if (capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
                playrAsHorizontalSpeed)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    void Die()
    {
        if (capsuleCollider2.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            rb2d.velocity = new Vector2(5f, 5f);
            animator.SetTrigger("Dead");
        }
    }
}
