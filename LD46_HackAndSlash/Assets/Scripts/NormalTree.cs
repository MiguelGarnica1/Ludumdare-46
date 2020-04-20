using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    public GameObject woodPrefab;
    public float attackDamage;

    public override void Die()
    {
        Instantiate(woodPrefab, transform.position, Quaternion.identity);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(attackDamage);
        }
    }
}
