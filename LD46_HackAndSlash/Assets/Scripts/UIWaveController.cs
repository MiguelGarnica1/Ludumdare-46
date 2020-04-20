using UnityEngine;
using TMPro;

public class UIWaveController : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    TextMeshProUGUI waveCountDownText;

    [SerializeField]
    TextMeshProUGUI waveCountText;

    private WaveSpawner.SpawnState previousState;
    // Use this for initialization
    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("No Spawner Referenced");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No Spawner Referenced");
            this.enabled = false;
        }
        if (waveCountDownText == null)
        {
            Debug.LogError("No Spawner Referenced");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogError("No Spawner Referenced");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.state)
        {
            case WaveSpawner.SpawnState.COUNTING:
                waveCountDownText.enabled = true;
                UpdateCountdownUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                waveCountDownText.enabled = false;
                UpdateSpawningUI();
                break;
        }

        previousState = spawner.state;
    }

    void UpdateCountdownUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
            Debug.Log("Counting");
        }
        waveCountDownText.text = ((int)spawner.waveCountDown).ToString();

    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.waves[spawner.currentWave].name;

            Debug.Log("Spawning");
        }
    }
}
