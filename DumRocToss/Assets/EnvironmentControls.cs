using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentControls : MonoBehaviour
{
    public Sprite[] backgrounds;
    public AudioClip[] musicList;

    public int spriteNum;
    public SpriteRenderer art;

    public void Start()
    {
        art = GetComponent<SpriteRenderer>();
    }

    public void SetBackground(int value)
    {
        if (spriteNum == value)
        { 
            return;
        } 

        spriteNum = value;

        art.sprite = backgrounds[spriteNum];
        var Aus = GameObject.Find("Music");
        Aus.GetComponent<AudioSource>().clip = musicList[spriteNum];
    }
}
