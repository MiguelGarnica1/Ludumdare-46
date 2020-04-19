using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public KeyCode left, right, up, down;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        int totalSpeed =  Math.Abs(Convert.ToInt32(rb.velocity.x + rb.velocity.y));
        // assign animator to player velocity
        animator.SetInteger("speed", totalSpeed);

        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if(Input.GetKey(right))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey(up))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (Input.GetKey(down))
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

    }
}
