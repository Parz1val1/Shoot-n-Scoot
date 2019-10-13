using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelUI : MonoBehaviour
{
    private string playText = "►", pauseText = "II", menu = "StartScene";
    public GameObject pauseScreen;
    public TextMeshProUGUI pauseButton;
    public Image[] hearts = new Image[3];
    public TextMeshProUGUI arrows;
    public int arrowNum, playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        arrowNum = Player.arrows;
        playerHealth = Player.health;
        arrows.text = ("x" + arrowNum);
    }

    // Update is called once per frame
    void Update()
    {
        updateArrows();
        updateHealth();
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

}
