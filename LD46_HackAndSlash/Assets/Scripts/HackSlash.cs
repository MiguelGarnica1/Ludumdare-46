using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSlash : MonoBehaviour
{
    public KeyCode attack;
    public float attackDamage, attackRate;

    private bool isAttacking;
    private BoxCollider2D bc;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        bc = this.GetComponent<BoxCollider2D>();
        bc.enabled = true;

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
        if (Input.GetKeyDown(attack) && !isAttacking)
        {
            animator.SetTrigger("slash");
            isAttacking = true;
            bc.enabled = true;
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isAttacking)
        {
            StartCoroutine(HitTimer(collision));
        }
    }

    IEnumerator HitTimer(Collider2D other)
    {
        other.GetComponent<Enemy>().GetDamaged(attackDamage);
        other.GetComponent<Enemy>().Knockback(this.transform);
        bc.enabled = false;
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
    }
}
