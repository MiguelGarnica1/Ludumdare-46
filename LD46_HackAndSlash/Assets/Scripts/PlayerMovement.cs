using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public KeyCode left, right, up, down;
    private Animator animator;
    private AudioSource audioSource;

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

        // animation and sfx
        int totalSpeed =  Convert.ToInt32(Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.y));
        animator.SetInteger("speed", totalSpeed);

        if (totalSpeed > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();

        }
        
        if(totalSpeed < 0)
        {
            audioSource.Stop();
        }

        /// check for death
        
        if(health.GetHealth() <= 0)
        {
            SceneManager.LoadScene("Dead");
        }

        /// movement 
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Knockback(collision.transform);
        }
    }

    public void Knockback(Transform other)
    {
        Vector2 diff = this.transform.position - other.position;
        diff *= 0.5f;

        this.transform.position = new Vector2(this.transform.position.x + diff.x, this.transform.position.y + diff.y);
    }
}
