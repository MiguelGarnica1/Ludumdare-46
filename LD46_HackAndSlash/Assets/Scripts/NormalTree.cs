using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    public GameObject woodPrefab;
    public float attackDamage;

    public override void Die()
    {
        float oneIn4 = Random.Range(1, 5);
        if (oneIn4 == 1)
        {
            Instantiate(woodPrefab, transform.position, Quaternion.identity);
        }
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
        health-= damage;
        if (health <= 0)
        {
            Die();
            RampageController.rampageCounter++;
            RampageController.resetCounter();
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
