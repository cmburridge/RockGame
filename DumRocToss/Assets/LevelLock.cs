using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class LevelLock : MonoBehaviour
{
    public LevelData lD;
    public UnityEvent isFinished;
    public SpriteRenderer sR;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (lD.completed == true)
        {
            sR.color = Color.clear;
            isFinished.Invoke();
        }
    }
}
