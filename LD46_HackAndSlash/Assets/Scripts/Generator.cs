﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    public int fuel;
    public Transform fire;
    public float burnRate;

    private float scaleMax = 10f;
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

        if (fuel < 10)
        {
            fire.localScale = new Vector3(fuel/scaleMax, fuel / scaleMax, 1f);
        }

        if (fuel == 0)
        {
            fuel = -1;
            empty();
        }
    }

    public void empty()
    {
        SceneManager.LoadScene("NoFuel");
    }

    public void fill()
    {
        fuel++;

        foreach (ParticleSystem particleSys in pr)
        {
            particleSys.Play();
        }
    }

    public void editBurnRate(float rate)
    {
        burnRate = rate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            fill(); 
        }
    }
}
