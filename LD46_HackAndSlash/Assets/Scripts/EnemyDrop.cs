using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    private Inventory inventory;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public void collect()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inventory.inventory < inventory.limit)
            {
                Destroy(gameObject);
            }
        }
    }
}
