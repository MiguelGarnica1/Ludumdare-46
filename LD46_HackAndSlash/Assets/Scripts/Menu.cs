using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource.volume < 1)
        {
            audioSource.volume += 0.1f * Time.deltaTime;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Miguel");
    }
}
