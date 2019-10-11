using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private string level1 = "Level1", level2 = "Level2", level3 = "Level3", level4 = "Level4";
    public GameObject startUI, levelSelectUI;

    // Start is called before the first frame update
    void Start()
    {
        levelSelectUI.SetActive(false);
        startUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene(level1);
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene(level2);
    }

    public void loadLevel3()
    {
        SceneManager.LoadScene(level3);
    }

    public void loadLevel4()
    {
        SceneManager.LoadScene(level4);
    }

    public void play()
    {
        startUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }
}
