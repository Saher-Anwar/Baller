using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private LineRenderer lineRenderer;
    private float mouseXPosOnClick;
    private float mouseYPosOnClick;
    private float differenceInX, differenceInY;

    private bool isMouseUp = false;
    private bool contactMade = false;
    private bool hasJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPoint;
        Vector3 endPoint;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!hasJumped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseXPosOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                mouseYPosOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                lineRenderer.enabled = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                float mouseUpXPos;
                float mouseUpYPos;

                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                mouseUpXPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                mouseUpYPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                differenceInX = mouseXPosOnClick - mouseUpXPos;
                differenceInY = mouseYPosOnClick - mouseUpYPos;

                isMouseUp = true;
                lineRenderer.enabled = false;
            }

            if (Input.GetMouseButton(0))
            {
                Time.timeScale = 0.2f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                startPoint = gameObject.transform.position;
                startPoint.z = 0;
                lineRenderer.SetPosition(0, startPoint);
                endPoint = mousePos;
                endPoint.z = 0;
                lineRenderer.SetPosition(1, endPoint);
            }
        }
    }

    void FixedUpdate()
    {
        if (isMouseUp)
        {
            rigidbody2D.velocity = new Vector2(differenceInX*3, differenceInY*3);
            isMouseUp = false;
            hasJumped = true;
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
        hasJumped = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contactMade = false;
    }

    public void pushUp()
    {

        rigidbody2D.velocity = new Vector2(0, 15f);
    }

}
