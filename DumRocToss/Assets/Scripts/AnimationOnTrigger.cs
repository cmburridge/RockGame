using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnTrigger : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim = (Animator) GetComponent(typeof(Animator));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.Play("Still");
    }
}
