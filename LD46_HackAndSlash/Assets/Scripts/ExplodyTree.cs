using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodyTree : Enemy
{
    public Transform explodeEffect;
    public float explodeCountDown;

    private bool playerWithinRange = false;
    private bool dead;
    

    public override void Update()
    {
        base.Update();
        if (dead)
        {
            explodeCountDown -= Time.deltaTime;
            if (explodeCountDown <= 0)
            {
                Explode();
            }
        }
    }

    public override void Attack(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        dead = true;
        moveSpeed = 0;
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
    }

    void Explode()
    {
        if (playerWithinRange)
        {
            Debug.Log("Explosion hit player!");
        }
        Transform clone = Instantiate(explodeEffect, transform.position, transform.rotation);
        Destroy(clone.gameObject, 1f);
        Destroy(gameObject);
        WaveSpawner.numOfEnemy--;
    }

    //Check if player within explosion radius
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerWithinRange = true;
            Debug.Log("Player!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerWithinRange = false;
            Debug.Log("Player Nah!");
        }
    }

    //Knockback
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !dead)
        {
            Vector2 diff = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
        }
    }
}
