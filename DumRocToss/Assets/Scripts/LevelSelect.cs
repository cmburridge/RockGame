using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int num;
    
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneBuildIndex: num);
    }
}
