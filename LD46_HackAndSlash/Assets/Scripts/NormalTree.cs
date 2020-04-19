using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    public GameObject woodPrefab;

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
}
