using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public Level GetLevel()
    {
        return new Level();
    } 
}

public class Level
{
    bool win = false;
    public GameObject square;
    public GameObject ball;


}
