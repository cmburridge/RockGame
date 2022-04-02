using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public PlayerData player;
    public GameObject thisCostume;
    private SpriteRenderer ren;

    private void Start()
    {
        ren = (SpriteRenderer) GetComponent(typeof(SpriteRenderer));
    }

    private void OnMouseDown()
    {
        player.costume = thisCostume;
    }

    private void Update()
    {
        if (player.costume == thisCostume)
        {
            ren.color = Color.green;
        }
        else
        {
            ren.color = Color.white;
        }
    }
}
