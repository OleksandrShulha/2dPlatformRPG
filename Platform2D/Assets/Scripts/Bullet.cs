using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    [SerializeField] private float moveSpeed = 5f;
    private PlayrMove playr;
    private float xSpeed;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playr = FindAnyObjectByType<PlayrMove>();
        xSpeed = playr.transform.localScale.x * moveSpeed;

    }


    void Update()
    {
        myRigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);

    }
}
