using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public override void moveTowardTarget(Transform target)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        WaveSpawner.numOfEnemy--;
        Destroy(gameObject,0.5f);
    }
}
