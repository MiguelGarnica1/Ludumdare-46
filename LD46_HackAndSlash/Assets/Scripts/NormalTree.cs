﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    public override void Die()
    {
        Destroy(gameObject, 10f);
        WaveSpawner.numOfEnemy--;
    }

    public override void Attack(float damage)
    {
        //ChangeColor();
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
    }
}
