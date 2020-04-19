using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSlash : MonoBehaviour
{
    public KeyCode attack;
    public float attackDamage, attackRate;

    private bool isAttacking;
    private float attackCounter;
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

        if (isAttacking)
        {
            attackCounter -= Time.deltaTime ;
            if (attackCounter <= 0)
            {
                isAttacking = false;
                bc.enabled = false;
            }
        }

        // attack when button pressed
        if (Input.GetKeyDown(attack))
        {
            Debug.Log("isAtacking: " + isAttacking);
            if (!isAttacking)
            {
                attackCounter = attackRate;
                isAttacking = true;
                bc.enabled = true;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                {
                    return;
                }
                else
                {
                    animator.SetTrigger("slash");
                }
            }
            
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isAttacking)
        {
            other.GetComponent<Enemy>().GetDamaged(attackDamage);
            other.GetComponent<Enemy>().Knockback(this.transform);
        }
        bc.enabled = false;
    }

}
