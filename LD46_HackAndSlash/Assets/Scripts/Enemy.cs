﻿using System.Collections;
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
    protected Color defaultColor;

    public abstract void Die();
    public abstract void Attack(float damage);
    public abstract void GetDamaged(float damage);

    

    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        defaultColor = GetComponent<SpriteRenderer>().color;
    }

    public virtual void Update()
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
        isColorChanged = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 0, 0, .7f);

    }
}
