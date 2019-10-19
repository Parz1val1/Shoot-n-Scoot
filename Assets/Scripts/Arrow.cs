using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector2(0, .5f));
        foreach(GameObject g in enemies)
        {
            if(Vector2.Distance(this.transform.position, g.transform.position) < 1)
            {
                g.SetActive(false);
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                Player.kills++;
            }
        }
    }
}
