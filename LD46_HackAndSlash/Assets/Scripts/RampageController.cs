using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RampageController : MonoBehaviour
{
    public enum RampageState {powerUp, counting}

    public static float rampageMultiplier = 0.0f;
    public static int rampageCounter = 0;

    [SerializeField]
    private Transform axe;
    [SerializeField]
    private TextMeshProUGUI rampMultiText;
    [SerializeField]
    private TextMeshProUGUI rampCounter;
    [SerializeField]
    private RampageState rampageState = RampageState.counting;
    private static float rampageResetCounter = 5f;
    private int rampageMultiUpperLimit;
    private bool rampageIncrease;

    // Start is called before the first frame update
    void Start()
    {
        rampageResetCounter = 5f;
        rampageMultiUpperLimit = 10;
    }

    // Update is called once per frame
    void Update()
    {
        rampMultiText.text = rampageMultiplier.ToString() + "x";
        if (rampageResetCounter <= 0)
        {
            rampageMultiplier = 0f;
            rampageMultiUpperLimit = 10;
            axe.GetComponent<HackSlash>().attackDamage = 5f;
            axe.GetComponent<HackSlash>().attackRate = 1f;
        }

        if (rampageCounter >= rampageMultiUpperLimit)
        {
            rampageState = RampageState.powerUp;
            rampageCounter = 0;
        }
        switch (rampageState)
        {
            case RampageState.counting:
                rampageResetCounter -= Time.deltaTime;
                rampCounter.text = rampageResetCounter.ToString() + ", " + rampageCounter;
                break;
            case RampageState.powerUp:
                rampageIncrease = true;
                rampageMultiplier += 0.1f;
                rampageMultiUpperLimit *= 2;
                rampageState = RampageState.counting;
                break;
        }

        if (rampageIncrease)
        {
            rampagePowerUp();
        }
    }

    public static void resetCounter()
    {
        rampageResetCounter = 5f;
    }

    void rampagePowerUp()
    {
        axe.GetComponent<HackSlash>().attackDamage += 5 * rampageMultiplier;
        if (axe.GetComponent<HackSlash>().attackRate > 0.1f) //MAX AT 0.1
        {
            float tempSpeed = axe.GetComponent<HackSlash>().attackRate;
            axe.GetComponent<HackSlash>().attackRate -= tempSpeed * rampageMultiplier;
        }
        rampageIncrease = false;
    }
}
