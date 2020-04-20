using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodyTree : Enemy
{
    public Transform explodeEffect;
    public float explodeCountDown;
    public GameObject woodPrefab;

    public float attackDamage;

    private bool playerWithinRange = false;
    private bool dead;
    private SpriteRenderer baseColor;
    public override void Start()
    {
        base.Start();
        baseColor = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
        if (dead)
        {
            explodeCountDown -= Time.deltaTime;
            baseColor.color = Color.Lerp(defaultColor, Color.black, Mathf.PingPong(Time.time, 1));
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
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
            RampageController.rampageCounter++;
            RampageController.resetCounter();
        }
    }

    void Explode()
    {
        if (playerWithinRange)
        {
            Debug.Log("Explosion hit player!");
        }
        Transform clone = Instantiate(explodeEffect, transform.position, transform.rotation);
        Destroy(clone.gameObject, 1f);
        float oneIn4 = Random.Range(1, 5);
        if (oneIn4 == 1)
        {
            Instantiate(woodPrefab, transform.position, Quaternion.identity);
        }
        WaveSpawner.numOfEnemy--;
        Destroy(gameObject); 
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(attackDamage);
        }
    }
}
