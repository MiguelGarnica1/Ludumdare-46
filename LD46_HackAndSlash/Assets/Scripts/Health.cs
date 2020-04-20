using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float max_health;
    private float hp;

    private SpriteRenderer sp;
    private bool isColorChanged;
    private float resetColorTime = .15f;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        hp = max_health;
        sp = GetComponent<SpriteRenderer>();
        defaultColor = sp.color;
    }

    // Update is called once per frame
    void Update()
    {
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
    

    public void Damage(float damage)
    {
        hp -= damage;
        ChangeColor();
    }


    public float GetHealth()
    {
        return hp;
    }

    /*
     * Turn Red when it is attacked
     */
    private void ChangeColor()
    {
        isColorChanged = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 0, 0, .7f);

    }

}
