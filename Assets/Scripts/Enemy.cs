using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] points;
    public int current;
    public static bool move;
    public bool forward;
    public int turns;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        turns = 1;
        forward = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(turns != LevelUI.turns)
        {
            enemyMove();
            turns = LevelUI.turns;
        }
    }

    public void enemyMove()
    {
        if (current == 0)
        {
            forward = true;
        }
        else if (current == points.Length - 1)
        {
            forward = false;
        }
        if (forward)
        {
            iTween.MoveTo(enemy, points[current].transform.position, 1);
            current++;
        }
        else
        {
            iTween.MoveTo(enemy, points[current].transform.position, 1);
            current--;
        }
    }
}
