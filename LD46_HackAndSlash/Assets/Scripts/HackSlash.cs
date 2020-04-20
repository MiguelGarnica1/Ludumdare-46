using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSlash : MonoBehaviour
{
    public KeyCode attack;
    public float attackDamage, attackRate, attackRange = 0.5f;
    float nextAttackTime = 0.0f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    private Animator animator;

    public GameObject player;
    private Vector3 v3pos;
    private float angle = 0f;
    private float distance = 0.5f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        attackRate = 1f;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    float timer;


    // Update is called once per frame
    void Update()
    {
        v3pos = Input.mousePosition;
        v3pos.z = player.transform.position.z - Camera.main.transform.position.z;
        v3pos = Camera.main.ScreenToWorldPoint(v3pos);
        v3pos = v3pos - player.transform.position;
        float angle = Mathf.Atan2(v3pos.y, v3pos.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        transform.localEulerAngles = new Vector3(0, 0, angle-90);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
        transform.localPosition = new Vector3(player.transform.position.x + xPos, player.transform.position.y + yPos, 0);
        //Get the Screen positions of the object
        // Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        //float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(attack))   
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                audioSource.Play();
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

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().GetDamaged(attackDamage);
            enemy.GetComponent<Enemy>().Knockback(player.transform);

        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}
