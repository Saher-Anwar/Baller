using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float mouseXPosOnClick;
    private float mouseYPosOnClick;
    private float differenceInX, differenceInY;
    private bool shouldAddForce = false;
    private bool contactMade = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (shouldAddForce)
        {
            rigidbody2D.velocity = new Vector2(differenceInX*3, differenceInY*3);
            shouldAddForce = false;
        }

        if (contactMade && rigidbody2D.velocity.x != 0) 
        {
            float frictionValue = 0.09f;
            if(rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = rigidbody2D.velocity - new Vector2(frictionValue, 0);
            }
            else
            {
                rigidbody2D.velocity = rigidbody2D.velocity + new Vector2(frictionValue, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contactMade = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contactMade = false;
    }

    private void OnMouseDown()
    {
        mouseXPosOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mouseYPosOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
    }

    private void OnMouseDrag()
    {
        // this will probably be used for trail
    }

    private void OnMouseUp()
    {
        float mouseUpXPos;
        float mouseUpYPos;

        mouseUpXPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mouseUpYPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        differenceInX = mouseXPosOnClick - mouseUpXPos;
        differenceInY = mouseYPosOnClick - mouseUpYPos;

        shouldAddForce = true;
    }
}
