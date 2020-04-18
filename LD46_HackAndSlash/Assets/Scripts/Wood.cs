using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float speed;
    public GameObject p1;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            p1.GetComponent<Inventory>().inventory++;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Generator"))
        {
            Destroy(gameObject);
        }
    }
}
