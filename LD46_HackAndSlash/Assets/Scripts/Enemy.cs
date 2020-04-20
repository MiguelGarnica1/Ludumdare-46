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
    protected Color defaultColor;

    public abstract void Die();
    public abstract void Attack(float damage);
    public abstract void GetDamaged(float damage);

    protected AudioSource audioSource;
    float timer, delay = 5;
    public AudioClip[] clips;
    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        defaultColor = GetComponent<SpriteRenderer>().color;

        audioSource = GetComponent<AudioSource>();
        delay = Random.Range(2f, 4f);
 
    }

    float dietimer = 0;
    int timesPlayed = 0;
    public virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = Random.Range(-2f, 2f);
                audioSource.Play();
                delay = Random.Range(2f, 4f);
                timer = 0;
            }
        }

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
            audioSource.pitch = 1;
            if (!audioSource.isPlaying && timesPlayed < 1)
            {
                audioSource.PlayOneShot(clips[1], 0.5f);
                timesPlayed++;
            }
            dietimer += Time.deltaTime;

            if(dietimer > 0.5f)
            {
                Die();
                dietimer = 0;
            }
           
        }
        //TODO: When health reach 0, die
        if (Input.GetKey(KeyCode.X))
        {
            Die();
            RampageController.resetCounter();
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
        transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y );
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
