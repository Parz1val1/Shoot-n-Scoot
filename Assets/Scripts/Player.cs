//script modified from anonymous programmer on Stackoverflow
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
public class Player : MonoBehaviour
{
    public static int health, arrows, coinCount, kills;
    public static bool paused, won;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private Scene scene;
    public bool detectSwipeOnlyAfterRelease = true;
    public float SWIPE_THRESHOLD = 20f;
    public GameObject coin1, coin2, coin3, enemy1, enemy2, enemy3, enemy4, finish;
    Animator playerAnimator;
    public TextMeshProUGUI test;

    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        scene = SceneManager.GetActiveScene();
        OnSceneLoaded(scene, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!string.Equals(scene.path, this.scene.path)) return;
        Debug.Log("Re-Initializing", this);
        won = false;
        health = 3;
        arrows = 2;
        coinCount = 0;
        paused = false;
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            ////Detects Swipe while finger is still moving
            //if (touch.phase == TouchPhase.Moved)
            //{
            //    if (!detectSwipeOnlyAfterRelease)
            //    {
            //        fingerDown = touch.position;
            //        checkSwipe();
            //    }
            //}

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
        if (Vector2.Distance(this.transform.position, finish.transform.position) < 1)
        {
            won = true;
            Debug.Log("won true");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !paused)
        {
            OnSwipeLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !paused)
        {
            OnSwipeRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !paused)
        {
            OnSwipeUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !paused)
        {
            OnSwipeDown();
        }

        if (coin1.active && Vector2.Distance(this.transform.position, coin1.transform.position) < 1)
        {
            coin1.SetActive(false);
            coinCount++;
            Debug.Log("coin1");
        }
        else if (coin2.active && Vector2.Distance(this.transform.position, coin2.transform.position) < 1)
        {
            coin2.SetActive(false);
            coinCount++;
            Debug.Log("coin2");
        }
        else if (coin3.active && Vector2.Distance(this.transform.position, coin3.transform.position) < 1)
        {
            coin3.SetActive(false);
            coinCount++;
            Debug.Log("coin3");
        }
        else if (enemy1.active && Vector2.Distance(this.transform.position, enemy1.transform.position) < 1)
        {
            enemy1.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy1");
        }
        else if (enemy2.active && Vector2.Distance(this.transform.position, enemy2.transform.position) < 1)
        {
            enemy2.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy2");
        }
        else if (enemy3.active && Vector2.Distance(this.transform.position, enemy3.transform.position) < 1)
        {
            enemy3.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy3");
        }
        else if (enemy4.active && Vector2.Distance(this.transform.position, enemy4.transform.position) < 1)
        {
            enemy4.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy4");
        }

        shoot();
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1 && !paused)
        {
            playerAnimator.SetTrigger("WalkUp");
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(0, -2, 0)), 1);
        }
        LevelUI.turns++;
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1 && !paused)
        {
            playerAnimator.SetTrigger("WalkDown");
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(0, 2, 0)), 1);
        }
        LevelUI.turns++;

    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1 && !paused)
        {
            playerAnimator.SetTrigger("WalkLeft");
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(2, 0, 0)), 1);
        }
        LevelUI.turns++;

    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1 && !paused)
        {
            playerAnimator.SetTrigger("WalkRight");
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(-2, 0, 0)), 1);
        }
        LevelUI.turns++;

    }

    //Not working
    void shoot()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Vector2 dir = Input.GetTouch(0).position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            test.text = ("Angle" + angle);
        }
    }



}
