using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public float damage;

    [SerializeField]
    protected float stoppingPoint;

    public abstract void Die();
    public abstract void Attack(float damage);

    protected Transform target;

    // Start is called before the first frame update
    protected void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Die();
    }

    protected void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingPoint)
        {
            moveTowardTarget(target);
        }
        //TODO: Get and find the player to attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetDamaged(damage);
        }
        if (health <= 0)
        {
            Die();
        }
        //TODO: When health reach 0, die
        if (Input.GetKey(KeyCode.X))
        {
            Die();
        }

        if (isColorChanged)
        {
            resetColorTime -= Time.deltaTime;
            if (resetColorTime <= 0)
            {
                isColorChanged = false;
                resetColorTime = 0.15f;
                //Reset back to default color
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                sr.color = defaultColor;
            }
        }
    }

    protected void moveTowardTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    /*
     * Turn Red when it is attacked
     */
    protected void ChangeColor()
    {
        float waitTime = 0.30f;//Time until color is return back to normal
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color defaultColor = sr.color;
        sr.color = new Color(1f, 0, 0, .7f);

        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        sr.color = defaultColor;
    }

}
