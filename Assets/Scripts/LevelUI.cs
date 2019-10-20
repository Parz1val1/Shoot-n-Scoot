using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelUI : MonoBehaviour
{
    private string playText = "►", pauseText = "II", menu = "StartScene";
    private Scene scene;
    public GameObject pauseScreen, winScreen, loseScreen;
    public TextMeshProUGUI pauseButton;
    public Image[] hearts = new Image[3];
    public GameObject[] coins = new GameObject[3];
    public TextMeshProUGUI arrows;
    public int arrowNum, playerHealth, coinNum;
    public static int turns;
    public static bool won;
        
    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        scene = SceneManager.GetActiveScene();
        OnSceneLoaded(scene, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (GameObject g in coins)
        {
            g.SetActive(false);
        }
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        arrowNum = Player.arrows;
        playerHealth = Player.health;
        coinNum = Player.coinCount;
        arrows.text = ("x" + arrowNum);
        turns = 0;
        Player.paused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        updateArrows();
        updateHealth();
        updateCoins();
        win(Player.won);
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

    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        if(playerHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(SoundManager.lose, transform.position);
            loseScreen.SetActive(true);
            Player.paused = true;
            Time.timeScale = 0;
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

    public void win(bool won)
    {
        if (won)
        {
            AudioSource.PlayClipAtPoint(SoundManager.win, transform.position);
            winScreen.SetActive(true);
            Player.paused = true;
            Time.timeScale = 0;
        }
    }

}
