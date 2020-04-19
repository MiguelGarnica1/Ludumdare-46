using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterTree : Enemy
{
    public GameObject miniTree;

    [SerializeField]
    private int minCount = 3;
    [SerializeField]
    private int maxCount = 6;

    public override void Attack(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        WaveSpawner.numOfEnemy--;
        Spawn();
        Destroy(gameObject);
    }

    public override void GetDamaged(float damage)
    {
        ChangeColor();
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

    //Knockback
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Vector2 diff = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
        }
    }
}
