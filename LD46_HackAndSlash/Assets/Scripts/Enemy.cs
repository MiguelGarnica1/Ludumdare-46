using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float moveSpeed;

    public abstract void moveTowardTarget(Transform target);
    public abstract void Die();
    

}
