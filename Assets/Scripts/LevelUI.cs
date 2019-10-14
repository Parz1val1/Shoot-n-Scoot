using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelUI : MonoBehaviour
{
    private string playText = "►", pauseText = "II", menu = "StartScene";
    public GameObject pauseScreen, winScreen;
    public TextMeshProUGUI pauseButton;
    public Image[] hearts = new Image[3];
    public GameObject[] coins = new GameObject[3];
    public TextMeshProUGUI arrows;
    public int arrowNum, playerHealth, coinNum;
    public static int turns;
    public static bool won;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in coins)
        {
            g.SetActive(false);
        }
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        arrowNum = Player.arrows;
        playerHealth = Player.health;
        coinNum = Player.coinCount;
        arrows.text = ("x" + arrowNum);
        turns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateArrows();
        updateHealth();
        updateCoins();
    }

    public void pause()
    {
        if (Player.paused)
        {
            Player.paused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            pauseButton.text = pauseText;
        }
        else
        {
            Player.paused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            pauseButton.text = playText;
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(menu);
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void loadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void loadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void updateArrows()
    {
        if (Player.arrows != arrowNum)
        {
            arrowNum = Player.arrows;
            arrows.text = ("x" + arrowNum);
        }
    }

    public void updateHealth()
    {
        if(playerHealth != Player.health)
        {
            playerHealth = Player.health;
            hearts[playerHealth].sprite = Resources.Load<Sprite>("Tiny RPG Forest/Artwork/sprites/misc/hearts/hearts-2");
        }
    }

    public void updateCoins()
    {
        if (coinNum != Player.coinCount)
        {
            coinNum = Player.coinCount;
            coins[coinNum-1].SetActive(true);
            Debug.Log("test");
        }
    }

}
