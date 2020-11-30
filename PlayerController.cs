using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float moveSpeedY;
    public float moveSpeedInitial;
    public float moveSpeedInitialY;
    public float moveSpeedSprint;
    public float moveSpeedSprintY;
    public bool moveEnabled;
    public bool isMoving;

    public Rigidbody2D rb;
    public Vector3 movement;
    public float drag;
    public int[] ad = new int[2];
    public bool left;
    public bool up;

    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (moveEnabled)
        {
            Move();
        }

        Sprint();

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    void Sprint()
    {

        if (Input.GetKey("left shift"))
        {
            moveSpeedY = moveSpeedSprintY;
            moveSpeed = moveSpeedSprint;

        }
        else
        {
            moveSpeedY = moveSpeedInitialY;
            moveSpeed = moveSpeedInitial;
        }
    }

    void Move()
    {

        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {

            left = true;
        }

        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {

            left = false;
        }

        if ((Input.GetKeyUp("d") && Input.GetKey("a")) || (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)))
        {

            left = true;
        }

        if ((Input.GetKeyUp("a") && Input.GetKey("d")) || (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {

            left = false;
        }

        if ((Input.GetKey("d") && !Input.GetKey("a")) || (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            left = false;
        }

        if ((Input.GetKey("a") && !Input.GetKey("d")) || (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            left = true;
        }


        if (left)
        {
            movement.x = -moveSpeed;
            isMoving = true;
        }
        if (!left)
        {
            movement.x = moveSpeed;
            isMoving = true;
        }

        if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 0;

        }


        /////////////////////////////////////////
        ///



        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
        {

            up = true;
        }

        if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {

            up = false;
        }

        if ((Input.GetKeyUp("s") && Input.GetKey("w")) || (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKey(KeyCode.UpArrow)))
        {

            up = true;
        }

        if ((Input.GetKeyUp("w") && Input.GetKey("s")) || Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {

            up = false;
        }


        if (up)
        {
            movement.y = moveSpeedY;
            isMoving = true;
        }
        if (!up)
        {
            movement.y = -moveSpeedY;
            isMoving = true;
        }

        if ((!Input.GetKey("w") && !Input.GetKey("s")) && (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)))
        {
            movement.y = 0;

        }

        if (!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = false;
        }


        if (movement.x != 0)
        {

            movement.x = (float)Mathf.Lerp(movement.x, 0, drag);
        }

        if (movement.y != 0)
        {

            movement.y = (float)Mathf.Lerp(movement.y, 0, drag);
        }

    }

    
}
    
