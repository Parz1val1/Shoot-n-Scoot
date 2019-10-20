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
    public bool detectSwipeOnlyAfterRelease = true, tap;
    public float SWIPE_THRESHOLD = 40f, timer;
    public GameObject enemy1, enemy2, enemy3, enemy4, arrow, finish;
    public GameObject[] arrowSpawns, coins;
    Animator playerAnimator;
    int index = 0;

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
        if (scene.name == "Level1")
        {
            arrows = 2;
        }
        else if(scene.name == "Level2")
        {
            arrows = 3;
        }
        else if (scene.name == "Level3")
        {
            arrows = 3;
        }
        else if (scene.name == "Level4")
        {
            arrows = 3;
        }
        coinCount = 0;
        paused = false;
        playerAnimator = gameObject.GetComponent<Animator>();
        timer = 0;
        coins = GameObject.FindGameObjectsWithTag("coin");
    }

    void Update()
    {
        //if(timer <= 0)
        //{
        //    shoot();
        //}
        //else
        //{
        //    timer -= Time.deltaTime;
        //}

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                tap = true;
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
                if (Vector2.Distance(fingerDown, fingerUp) > SWIPE_THRESHOLD)
                {
                    tap = false;
                }
                checkSwipe();
                if (tap)
                {
                    shoot();
                }
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

        foreach (GameObject g in coins)
        {
            if(Vector2.Distance(this.transform.position, g.transform.position) < 1)
            {
                AudioSource.PlayClipAtPoint(SoundManager.coin, transform.position);
                g.SetActive(false);
                coinCount++;
                coins = GameObject.FindGameObjectsWithTag("coin");
            }
        }

        if (enemy1.active && Vector2.Distance(this.transform.position, enemy1.transform.position) < 1)
        {
            AudioSource.PlayClipAtPoint(SoundManager.hurt, transform.position);
            enemy1.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy1");
        }
        else if (enemy2.active && Vector2.Distance(this.transform.position, enemy2.transform.position) < 1)
        {
            AudioSource.PlayClipAtPoint(SoundManager.hurt, transform.position);
            enemy2.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy2");
        }
        else if (enemy3.active && Vector2.Distance(this.transform.position, enemy3.transform.position) < 1)
        {
            AudioSource.PlayClipAtPoint(SoundManager.hurt, transform.position);
            enemy3.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy3");
        }
        else if (enemy4.active && Vector2.Distance(this.transform.position, enemy4.transform.position) < 1)
        {
            AudioSource.PlayClipAtPoint(SoundManager.hurt, transform.position);
            enemy4.SetActive(false);
            kills++;
            health--;
            Debug.Log("enemy4");
        }
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
            LevelUI.turns++;
        }

    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
        Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1 && !paused)
        {
            playerAnimator.SetTrigger("WalkLeft");
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(2, 0, 0)), 1);
            LevelUI.turns++;
        }

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
            LevelUI.turns++;
        }

    }

    //Not working
    void shoot()
    {
        if (Input.touchCount > 0 && (verticalMove() < SWIPE_THRESHOLD || horizontalValMove() < SWIPE_THRESHOLD) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Vector2 shootDirection;
            shootDirection = Input.GetTouch(0).position;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - (Vector2)transform.position;
            float least = float.MaxValue;
            if (Vector2.Distance(shootDirection, Vector2.up) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.up);
                index = 0;
            }
            if (Vector2.Distance(shootDirection, Vector2.down) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.down);
                index = 1;
            }
            if (Vector2.Distance(shootDirection, Vector2.left) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.left);
                index = 2;
            }
            if (Vector2.Distance(shootDirection, Vector2.right) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.right);
                index = 3;
            }
            if (arrows > 0)
            {
                if (index == 0)
                {
                    playerAnimator.SetTrigger("ShootUp");
                    Debug.Log("SHOOTUP");
                }
                else if (index == 1)
                {
                    playerAnimator.SetTrigger("ShootDown");
                }
                else if (index == 2)
                {
                    playerAnimator.SetTrigger("ShootLeft");
                }
                else if (index == 3)
                {
                    playerAnimator.SetTrigger("ShootRight");
                }
                //GameObject tempArrow = Instantiate(arrow, arrowSpawns[index].transform.position, arrowSpawns[index].transform.rotation) ;
                LevelUI.turns++;
                timer = 1;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - (Vector2)transform.position;
            float least = float.MaxValue;
            if (Vector2.Distance(shootDirection, Vector2.up) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.up);
                index = 0;
            }
            if (Vector2.Distance(shootDirection, Vector2.down) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.down);
                index = 1;
            }
            if (Vector2.Distance(shootDirection, Vector2.left) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.left);
                index = 2;
            }
            if (Vector2.Distance(shootDirection, Vector2.right) < least)
            {
                least = Vector2.Distance(shootDirection, Vector2.right);
                index = 3;
            }
            if (arrows > 0)
            {
                if (index == 0)
                {
                    playerAnimator.SetTrigger("ShootUp");
                    Debug.Log("SHOOTUP");
                }
                else if (index == 1)
                {
                    playerAnimator.SetTrigger("ShootDown");
                }
                else if (index == 2)
                {
                    playerAnimator.SetTrigger("ShootLeft");
                }
                else if (index == 3)
                {
                    playerAnimator.SetTrigger("ShootRight");
                }
                //GameObject tempArrow = Instantiate(arrow, arrowSpawns[index].transform.position, arrowSpawns[index].transform.rotation) ;
                LevelUI.turns++;
                timer = 1;
            }
        }
    }

    public void fire()
    {
        AudioSource.PlayClipAtPoint(SoundManager.shoot, transform.position);
        GameObject tempArrow = Instantiate(arrow, arrowSpawns[index].transform.position, arrowSpawns[index].transform.rotation);
        arrows--;
    }
}
