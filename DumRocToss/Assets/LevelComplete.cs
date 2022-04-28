using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public LevelData level;

    public void LevelCompleted()
    {
        level.completed = true;
    }
}
