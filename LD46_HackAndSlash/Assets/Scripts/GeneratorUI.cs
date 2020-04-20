using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratorUI : MonoBehaviour
{
    private Generator generator;
    private TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        generator = FindObjectOfType<Generator>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = generator.fuel.ToString();
    }
}
