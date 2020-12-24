using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private LineRenderer lineRenderer;
    private float scaleX;
    private float mouseXPosOnClick;
    private float mouseYPosOnClick;
    private float differenceInX, differenceInY;
    public float power = 3.5f;

    private bool isMouseUp = false;
    private bool contactMade = false;
    private bool hasJumped = false;
    private bool squeezeEffectFinished = false;

    // squeeze circle variables
    private float time = 0f;
    private float maxScaleTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        scaleX = transform.localScale.x;
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

            if (Input.GetMouseButton(0))
            {
                squeezeEffectFinished = false;
                Time.timeScale = 0.2f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                startPoint = gameObject.transform.position;
                startPoint.z = 0;
                lineRenderer.SetPosition(0, startPoint);
                endPoint = mousePos;
                endPoint.z = 0;
                lineRenderer.SetPosition(1, endPoint);

                // squeeze effect
                if (!squeezeEffectFinished)
                {
                    if(time <= maxScaleTime)
                    {
                        time += Time.deltaTime;
                        float timePercent = time / maxScaleTime;
                        float scale = Mathf.Lerp(scaleX, scaleX / 1.25f, timePercent);
                        Vector3 newScale = new Vector3(scale, transform.localScale.y, 1);
                        transform.localScale = newScale;
                    }
                }
            }


            if (Input.GetMouseButtonUp(0))
            {
                float mouseUpXPos;
                float mouseUpYPos;
                isMouseUp = true;
                lineRenderer.enabled = false;

                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                mouseUpXPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                mouseUpYPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                differenceInX = mouseXPosOnClick - mouseUpXPos;
                differenceInY = mouseYPosOnClick - mouseUpYPos;

                // set scale back to normal
                squeezeEffectFinished = true;
                transform.localScale = new Vector2(scaleX, transform.localScale.y);
                time = 0f;
            }

        }
    }

    void FixedUpdate()
    {
        if (isMouseUp)
        {
            rigidbody2D.velocity = new Vector2(differenceInX*power, differenceInY*power);
            isMouseUp = false;
            hasJumped = true;
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
        // TODO add a vector to the laready existing rigibody velocity
        rigidbody2D.velocity *= 2.5f;
    }

}
