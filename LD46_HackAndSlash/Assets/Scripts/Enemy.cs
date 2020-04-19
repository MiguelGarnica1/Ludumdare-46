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

        if (Input.GetKey(KeyCode.Space))
        {
            Attack(damage);
        }
    }

    protected void moveTowardTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void Knockback(Transform other)
    {
        Vector2 diff = transform.position - other.position;
        transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
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
