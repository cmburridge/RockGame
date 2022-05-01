using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownCheck : MonoBehaviour
{
    public LevelData lastLevel;
    public GameObject crown;

    private void Start()
    {
        if (lastLevel.completed == true)
        {
            crown.SetActive(true);
        }
    }
}
