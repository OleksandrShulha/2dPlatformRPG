using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider2D;
    Animator myAnimator;
    Transform playrPosition;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        playrPosition = FindAnyObjectByType<PlayrMove>().GetComponent<Transform>();
    }


    private void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);

        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            moveSpeed = -moveSpeed;
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Playr")
        {
            myAnimator.SetBool("isMove", false);
            myAnimator.SetBool("isAtack", true);
            moveSpeed = 0f;
        }
        if(transform.position.x > playrPosition.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        }

        if (transform.position.x < playrPosition.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        }
    }
}
