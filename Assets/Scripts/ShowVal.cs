using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowVal : MonoBehaviour
{
    TextMeshProUGUI textVal; 
    // Start is called before the first frame update
    void Start()
    {
        textVal = GetComponent<TextMeshProUGUI>();
        textVal.text = GetComponentInParent<Slider>().value.ToString();
    }

    // Update is called once per frame
    public void textUpdate(float value)
    {
        textVal.text = value.ToString();
    }
}
