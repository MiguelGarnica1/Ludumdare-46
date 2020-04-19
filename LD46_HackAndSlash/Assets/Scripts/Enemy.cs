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

    protected Transform target;
    protected bool isColorChanged;
    protected float resetColorTime = .15f;

    public abstract void Die();
    public abstract void Attack(float damage);
    public abstract void GetDamaged(float damage);

    

    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public virtual void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingPoint)
        {
            moveTowardTarget(target);
        }

        //TODO: Get and find the player to attack
        if (Input.GetKey(KeyCode.Space))
        {
            GetDamaged(damage);
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
                sr.color = new Color(1f, 0, 1f, .7f);
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
        isColorChanged = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 0, 0, .7f);

    }
}
