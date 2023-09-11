using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    public Renderer rend;
    private bool gameRunning = false;

    //walls in order of:
    //0 : Floor
    //1 : Right Wall
    //2 : Left Wall
    //3 : Ceiling
    public GameObject[] walls;
    public GameObject wallStorage;
    private float velocityX = 0.0f;
    private float velocityY = 0.0f;
    public float speed;
    public float drag;
    public float bounceLoss;
    private bool[] touchingWalls;
    //0 : Floor || 1 : Right Wall || 2 : Left Wall || 3 : Ceiling ||
    private bool[] touchingWallsType = { false, false, false, false };

    //Score
    public int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        walls = wallStorage.GetComponent<AllWalls>().walls;
        touchingWalls = new bool[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            touchingWalls[i] = false;
        }
    }

    public void StartGame()
    {
        rend.enabled = true;
        gameRunning = true;
    }

    private void FixedUpdate()
    {
        if (gameRunning)
        {
            //Update playerMovement based on user input
            bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        
            CheckCollision();
        
            CheckDirection(right, left, up, down);

            BallDrag();

            transform.position = new Vector3(transform.position.x + velocityX, transform.position.y + velocityY, transform.position.z);
        }

    }

    //Sets the velocities to their new numbers
    public void CheckDirection(bool right, bool left, bool up, bool down)
    {
        if (down && !touchingWallsType[0])
        {
            velocityY -= speed;
        }
        if (right && !touchingWallsType[1])
        {
            velocityX += speed;
        }
        if (left && !touchingWallsType[2])
        {
            velocityX -= speed;
        }
        if (up && !touchingWallsType[3])
        {
            velocityY += speed;
        }
    }

    //Checks if the player has hit a wall, and if so, bounces them away from it
    public void CheckCollision()
    {
        bool bouncedHorizontal = false;
        bool bouncedVertical = false;
        for (int i = 0; i < walls.Length; i++)
        {
            if (GetComponent<Collider2D>().IsTouching(walls[i].GetComponent<Collider2D>()))
            {
                if (touchingWalls[i] == false)
                {
                    Debug.Log("Touching Wall " + i);
                    touchingWalls[i] = true;
                    touchingWallsType[CheckWallType(walls[i].tag)] = true;
                    if ((walls[i].CompareTag("Ceiling") || walls[i].CompareTag("Floor")) || (walls[i].CompareTag("Ceiling Edge") || walls[i].CompareTag("Floor Edge")) && bouncedVertical == false)
                    {
                        velocityY = -velocityY / Mathf.Abs(bounceLoss + 1);
                        score++;
                        bouncedVertical = true;
                        if (walls[i].CompareTag("Ceiling Edge") || walls[i].CompareTag("Floor Edge"))
                        {
                            bouncedHorizontal = true;
                        }
                    }
                    else if ((walls[i].CompareTag("Left Wall") || walls[i].CompareTag("Right Wall")) || (walls[i].CompareTag("Left Wall Edge") || walls[i].CompareTag("Right Wall Edge")) && bouncedHorizontal == false)
                    {
                        velocityX = -velocityX / Mathf.Abs(bounceLoss + 1);
                        score++;
                        bouncedHorizontal = true;
                        if (walls[i].CompareTag("Left Wall Edge") || walls[i].CompareTag("Right Wall Edge"))
                        {
                            bouncedVertical = true;
                        }
                    }
                    
                }
            }
            else
            {
                touchingWalls[i] = false;
                if (!IsTouchingWalls())
                {
                    touchingWallsType[CheckWallType(walls[i].tag)] = false;
                }
            }
        }

    }

    //Slows down the ball over time
    public void BallDrag()
    {
        velocityX = velocityX / (drag + 1);
        velocityY = velocityY / (drag + 1);
    }

    public int CheckWallType(string tag)
    {
        if (tag == "Floor" || tag == "Floor Edge")
        {
            return 0;
        }
        else if (tag == "Right Wall" || tag == "Right Wall Edge")
        {
            return 1;
        }
        else if (tag == "Left Wall" || tag == "Left Wall Edge")
        {
            return 2;
        }
        else if (tag == "Ceiling" || tag == "Ceiling Edge")
        {
            return 3;
        }
        return -1;
    }

    public bool IsTouchingWalls()
    {
        bool isTouching = false;
        for (int i = 0; i < touchingWalls.Length; i++)
        {
            if (touchingWalls[i])
            {
                isTouching = true;
            }
        }
        return isTouching;
    }

    //private void Update()
    //{
    //    //Update playerMovement based on user input
    //    bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    //    bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    //    bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    //    bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

    //    CheckForCollisions();

    //    if (right && !touchingWalls[1])
    //    {
    //        velocityX += speed;
    //    }
    //    if (left && !touchingWalls[2])
    //    {
    //        velocityX -= speed;
    //    }
    //    if (up && !touchingWalls[3])
    //    {
    //        velocityY += speed;
    //    }
    //    if (down && !touchingWalls[0])
    //    {
    //        velocityY -= speed;
    //    }

    //    if (touchingWalls[0] || touchingWalls[3] && recentBounce[0] == 0)
    //    {
    //        Debug.Log("Bouncing");
    //        velocityY = -velocityY / (bounceLoss + 1);
    //        recentBounce[0] = 100;
    //    }
    //    else if (touchingWalls[1] || touchingWalls[2] && recentBounce[1] == 0)
    //    {
    //        Debug.Log("Bouncing");
    //        velocityX = -velocityX / (bounceLoss + 1);
    //        recentBounce[1] = 100;
    //    }
    //    if (recentBounce[0] > 0)
    //    {
    //        recentBounce[0]--;
    //    }
    //    if (recentBounce[1] > 0)
    //    {
    //        recentBounce[1]--;
    //    }
    //reset the timer if the timers have not updated recently
    //if (matchingArrays(timerTouchingWalls,prevTimerTouchingWalls))
    //{
    //    lengthOfMatching++;
    //    if (lengthOfMatching > 2)
    //    {
    //        timerTouchingWalls = new int[] { 0, 0, 0, 0 };
    //    }
    //}
    //else
    //{
    //    lengthOfMatching = 0;
    //}
    //prevTimerTouchingWalls = timerTouchingWalls;
    //Stop the user from going through walls
    //    Debug.Log("VelocityX: " + velocityX + " VelocityY: " + velocityY);
    //    for (int i = 0; i < timerTouchingWalls.Length; i++)
    //    {
    //        if (timerTouchingWalls[i] >= 1000)
    //        {
    //            if (i == 0 && !up)
    //            {
    //                velocityY = 0;
    //            }
    //            else if (i == 1 && !left)
    //            {
    //                velocityX = 0;
    //            }
    //            else if (i == 2 && !right)
    //            {
    //                velocityX = 0;
    //            }
    //            else if (i == 3 && !down)
    //            {
    //                velocityY = 0;
    //            }
    //        }
    //    }

    //    velocityX = velocityX / (drag + 1);
    //    velocityY = velocityY / (drag + 1);
    //    //Move the player based on the current status of playerMovement
    //    transform.position = new Vector3(transform.position.x + velocityX, transform.position.y + velocityY, transform.position.z);
    //}

    //public void CheckForCollisions()
    //{
    //    for (int i = 0; i < touchingWalls.Length; i++)
    //    {
    //        touchingWalls[i] = GetComponent<Collider2D>().IsTouching(walls[i].GetComponent<Collider2D>());

    //        if (touchingWalls[i])
    //        {
    //            timerTouchingWalls[i]++;
    //        }
    //        else
    //        {
    //            timerTouchingWalls[i] = 0;
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject == walls[0] || collision.gameObject == walls[3])
    //    {
    //        velocityY = -velocityY / (bounceLoss + 1);
    //    }
    //    else if (collision.gameObject == walls[1] || collision.gameObject == walls[2])
    //    {
    //        velocityX = -velocityX / (bounceLoss + 1);
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    touchingWalls[0] = touchingWalls[0] || collision.gameObject == walls[0];
    //    if (touchingWalls[0])
    //        timerTouchingWalls[0] += 1;
    //    else
    //        timerTouchingWalls[0] = 0;
    //    touchingWalls[1] = touchingWalls[1] || collision.gameObject == walls[1];
    //    if (touchingWalls[1])
    //        timerTouchingWalls[1] += 1;
    //    else
    //        timerTouchingWalls[1] = 0;
    //    touchingWalls[2] = touchingWalls[2] || collision.gameObject == walls[2];
    //    if (touchingWalls[2])
    //        timerTouchingWalls[2] += 1;
    //    else
    //        timerTouchingWalls[2] = 0;
    //    touchingWalls[3] = touchingWalls[3] || collision.gameObject == walls[3];
    //    if (touchingWalls[3])
    //        timerTouchingWalls[3] += 1;
    //    else
    //        timerTouchingWalls[3] = 0;

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    touchingWalls[0] = false;
    //    touchingWalls[1] = false;
    //    touchingWalls[2] = false;
    //    touchingWalls[3] = false;
    //}

    //public bool matchingArrays(int[] x, int[] y)
    //{
    //    bool matching = true;
    //    for (int i = 0; i < x.Length; i++)
    //    {
    //        if (!(x[i] == y[i]))
    //            matching = false;
    //    }
    //    return matching;
    //}
}
