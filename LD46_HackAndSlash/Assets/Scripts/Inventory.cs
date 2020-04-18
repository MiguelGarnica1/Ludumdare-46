using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventory;
    public Transform dropPoint;
    public GameObject woodPrefab;
    public KeyCode drop;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(drop))
        {
            if (inventory >= 1)
            {
                Instantiate(woodPrefab, dropPoint.position, dropPoint.rotation);
                inventory--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            inventory++;
        }
    }
}
