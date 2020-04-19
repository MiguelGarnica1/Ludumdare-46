using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventory;
    public int limit;
    public float throwRate;
    public Transform dropPoint;
    public GameObject woodPrefab;
    public KeyCode drop;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(drop) && timer > throwRate)
        {
            if (inventory >= 1)
            {
                Instantiate(woodPrefab, dropPoint.position, Quaternion.identity);
                inventory--;
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wood") && inventory < limit)
        {
            inventory++;
            var col = collision.gameObject.GetComponent<Wood>();
            col.collect();
        }

        if (collision.gameObject.CompareTag("EnemyDrop") && inventory < limit)
        {
            inventory++;
            var col = collision.gameObject.GetComponent<EnemyDrop>();
            col.collect();
        }
    }
}
