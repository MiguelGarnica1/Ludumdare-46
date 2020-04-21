using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RampageController : MonoBehaviour
{
    public enum RampageState { powerUp, counting, none, waiting }

    public static float rampageMultiplier = 0.0f;
    public static float rampageCounter = 0;
    public Animation resetRamp;

    public Slider rampageFillUp;

    [SerializeField]
    private Transform axe;
    [SerializeField]
    private TextMeshProUGUI rampMultiText;

    [SerializeField]
    private static RampageState rampageState = RampageState.counting;
    private static float rampageResetCounter = 5f;
    [SerializeField]
    private float rampageMultiUpperLimit;
    private Transform player;
    private bool rampageIncrease;
    private Animator rampageAnimation;
    [SerializeField]
    private Animator barAnim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rampageResetCounter = 3f;
        rampageMultiUpperLimit = 2;
        rampageAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rampageState == RampageState.waiting)
        {
            barAnim.SetBool("RampageBar", false);
        }
        if (rampageCounter >= rampageMultiUpperLimit)
        {
            rampageState = RampageState.powerUp;
            barAnim.SetBool("RampageBar", false);
            rampageCounter = 0;
        }

        if (rampageCounter > 0 && rampageState != RampageState.powerUp)
        {
            rampageResetCounter -= Time.deltaTime;
            if (rampageResetCounter <= 0)
            {
                rampageState = RampageState.counting;
            }
        }
        else if(rampageCounter < 0 && rampageState != RampageState.powerUp)
        {
            rampageCounter = 0;
            rampageResetCounter = 3f;
            barAnim.SetBool("RampageBar", false);
            rampageState = RampageState.none;
        }

        
        rampMultiText.text = rampageMultiplier.ToString() + "x";
        switch (rampageState)
        {
            case RampageState.none:
                axe.GetComponent<HackSlash>().attackDamage = 5f;
                axe.GetComponent<HackSlash>().attackRate = 1.5f;
                rampageMultiplier = 0;
                rampageAnimation.SetBool("RampageOn", false);
                break;
            case RampageState.counting:
                rampageCounter -= Time.deltaTime;
                barAnim.SetBool("RampageBar", true);
                break;
            case RampageState.powerUp:
                rampageIncrease = true;
                rampageAnimation.SetBool("RampageOn", true);
                rampageMultiplier += 0.1f;
                rampageMultiUpperLimit *= 2;
                rampageState = RampageState.waiting;
                break;
        }
        rampageFillUp.value = rampageCounter / rampageMultiUpperLimit;

        if (rampageIncrease)
        {
            rampagePowerUp();
        }
    }

    public static void resetCounter()
    {
        rampageResetCounter = 3f;
        rampageState = RampageState.waiting;
    }

    void rampagePowerUp()
    {
        axe.GetComponent<HackSlash>().attackDamage += 5 * rampageMultiplier;
        if (axe.GetComponent<HackSlash>().attackRate > 0.1f) //MAX AT 0.1
        {
            axe.GetComponent<HackSlash>().attackRate += 1 * rampageMultiplier;
        }
        if (rampageMultiUpperLimit > 20)
        {
            player.GetComponent<PlayerMovement>().speed++;
        }
        rampageIncrease = false;
    }

}
