using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text txt;
    public IntData cash;
    public int max;
    
    void Update()
    {
        if (cash.value > max)
        {
            cash.value = max;
        }
        txt.text = cash.value.ToString();
    }
}
