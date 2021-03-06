﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterTree : Enemy
{
    public GameObject miniTree;
    public GameObject woodPrefab;
    [SerializeField]
    private int minCount = 3;
    [SerializeField]
    private int maxCount = 6;
    public float attackDamage;

    public override void Attack(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        Spawn();
        WaveSpawner.numOfEnemy--;
        float oneIn2 = Random.Range(1, 3);
        if (oneIn2 == 1)
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

    public void Spawn()
    {
        int count = Random.Range(minCount, maxCount);
        for (int i = 0; i < count; i++)
        {
            Instantiate(miniTree, this.transform.position + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
            WaveSpawner.numOfEnemy++;
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
