using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : Enemy
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Die();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingPoint)
        {
            moveTowardTarget(target);
        }
    }

    public override void moveTowardTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public override void Die()
    {
        Destroy(gameObject, .5f);
        WaveSpawner.numOfEnemy--;
    }

    public override void Attack(int damage)
    {
        throw new System.NotImplementedException();
    }
}
