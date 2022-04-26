using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public UnityEvent mouseEvent;
    public int num;
    
    private void OnMouseUp()
    {
        mouseEvent.Invoke();
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(sceneBuildIndex: num);
    }
}
