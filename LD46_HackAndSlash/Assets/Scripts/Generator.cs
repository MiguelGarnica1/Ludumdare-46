using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int fuel;
    public float burnRate;
    private float timer;
    private Component[] pr;

    void Start()
    {
        pr = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > burnRate)
        {
            fuel--;
            timer = 0;
        }

        if (fuel == 0)
        {
            empty();
        }
    }

    public void empty()
    {
        //add action here
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            fuel++;

            foreach (ParticleSystem particleSys in pr)
            {
                particleSys.Play();
            }
        }
    }
}
