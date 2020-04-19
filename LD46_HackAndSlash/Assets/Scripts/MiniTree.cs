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
        Destroy(gameObject);
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
        health--;
    }
}
