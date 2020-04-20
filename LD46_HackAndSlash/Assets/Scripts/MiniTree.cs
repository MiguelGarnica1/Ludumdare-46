using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTree : Enemy
{
    public GameObject woodPrefab;
    public float attackDamage;

    public override void Attack(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        WaveSpawner.numOfEnemy--;
        float oneIn2 = Random.Range(1, 3);
        if (oneIn2 == 2)
        {
            Instantiate(woodPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
        health -= damage;
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
