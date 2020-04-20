﻿using System.Collections;
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
        Instantiate(woodPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
