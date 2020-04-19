using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTree : Enemy
{
    public override void Attack(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        WaveSpawner.numOfEnemy--;
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
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
