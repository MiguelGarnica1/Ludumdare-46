using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float max_health;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void Damage(float damage)
    {
        hp -= damage;
    }


    public float GetHealth()
    {
        return hp;
    }

}
