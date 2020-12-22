using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private float mouseXPosOnClick;
    private float mouseYPosOnClick;
    private float differenceInX, differenceInY;
    private bool shouldAddForce = false; 

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
            rigidbody2D.velocity = new Vector2(differenceInX*2, differenceInY*2);
            shouldAddForce = false;
        }
    }

    private void OnMouseDown()
    {
        mouseXPosOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mouseYPosOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        print("on click x: " + mouseXPosOnClick);
        print("on click y: " + mouseYPosOnClick);
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

        print("on up x: " + mouseUpXPos);
        print("on up y: " + mouseUpYPos);

        print("difference x: " + differenceInX);
        print("difference y: " + differenceInY);

        shouldAddForce = true;
    }
}
