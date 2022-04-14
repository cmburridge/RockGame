using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text txt;
    public IntData cash;
    
    void Update()
    {
        if (cash.value > 999)
        {
            cash.value = 999;
        }
        txt.text = cash.value.ToString();
    }
}
