using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodUI : MonoBehaviour
{
    private Inventory inventory;
    private TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        var amount = inventory.inventory;
        var limit = inventory.limit;
        text.text = amount + "/" + limit;
    }
}
