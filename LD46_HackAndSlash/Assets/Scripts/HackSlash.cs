using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSlash : MonoBehaviour
{
    public KeyCode attack;
    public float attackDamage, attackRate;
    private BoxCollider2D bc;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        bc = this.GetComponent<BoxCollider2D>();
        bc.enabled = false;

        animator = GetComponent<Animator>();
    }

    float timer;
    // Update is called once per frame
    void Update()
    { 
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));


       

        // attack when button pressed
        if (Input.GetKeyDown(attack) && !bc.enabled)
        {
            bc.enabled = true;
            animator.SetTrigger("slash");
        }

        if (bc.enabled)
        {
            timer += Time.deltaTime;
            if(timer > 1f/attackRate)
            {
                bc.enabled = false;
                timer = 0;
            }
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().Damage(attackDamage);
        }
    }

}
