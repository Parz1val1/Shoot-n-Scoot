using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int health, arrows;
    public static bool paused;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        arrows = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
