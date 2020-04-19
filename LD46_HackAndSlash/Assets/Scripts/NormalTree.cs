using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    public override void Die()
    {
        Destroy(gameObject);
        WaveSpawner.numOfEnemy--;
    }

    public override void Attack(float damage)
    {
        //ChangeColor();
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
        health--;
    }

   
    //Knockback
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Vector2 diff = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
        }
    }


}
