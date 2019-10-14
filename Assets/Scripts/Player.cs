//script modified from anonymous programmer on Stackoverflow
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int health, arrows, coinCount, kills;
    public static bool paused;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = true;
    public float SWIPE_THRESHOLD = 20f;
    public GameObject coin1, coin2, coin3;


    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        arrows = 2;
        coinCount = 0;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipeLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipeRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipeUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipeDown();
        }

        if (coin1 != null && Vector2.Distance(this.transform.position, coin1.transform.position) < 1)
        {
            coin1.SetActive(false);
            coin1 = null;
            coinCount++;
            Debug.Log("coin1");
        }
        else if (coin2 != null && Vector2.Distance(this.transform.position, coin2.transform.position) < 1)
        {
            coin2.SetActive(false);
            coin2 = null;
            coinCount++;
            Debug.Log("coin2");
        }
        else if (coin3 != null && Vector2.Distance(this.transform.position, coin3.transform.position) < 1)
        {
            coin3.SetActive(false);
            coin3 = null;
            coinCount++;
            Debug.Log("coin3");
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
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1)
        {
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(0, -2, 0)), 1);
        }
        LevelUI.turns++;
    }

    void OnSwipeDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1)
        {
            Debug.Log("Swipe Down");
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(0, 2, 0)), 1);
        }
        LevelUI.turns++;

    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1)
        {
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(2, 0, 0)), 1);
        }
        LevelUI.turns++;

    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
        //Debug.Log(Vector2.Distance(transform.position, hit.point));
        if (hit.collider.gameObject.tag == "Enemy" || Vector2.Distance(transform.position, hit.point) > 1)
        {
            iTween.MoveTo(this.gameObject, (this.transform.position - new Vector3(-2, 0, 0)), 1);
        }
        LevelUI.turns++;

    }


    //private void OnCollisionEnter2D(Collider2D collider)
    //{
    //    Debug.Log(collider.gameObject.tag);
    //    if(collider.gameObject.tag == "Finish")
    //    {
    //        LevelUI.won = true;
    //    }
    //    else if (collider.gameObject.tag == "Enemy")
    //    {
    //        health--;
    //        collider.gameObject.SetActive(false);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            LevelUI.won = true;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            health--;
            other.gameObject.SetActive(false);
        }
    }
}
