using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private bool wasBig = false;

    private Camera cam;
    private float zoomOutFactor = 3f;
    private float targetZoom;
    private float zoomLerpSpeed = 10f;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    private void FixedUpdate()
    {
        bool isXPositivelyFaster = player.GetComponent<Rigidbody2D>().velocity.x > 10;
        bool isXNegativelyFaster = player.GetComponent<Rigidbody2D>().velocity.x < -10;
        bool isYPositivelyFaster = player.GetComponent<Rigidbody2D>().velocity.y > 10;
        bool isYNegativelyFaster = player.GetComponent<Rigidbody2D>().velocity.y < -10;


        if (isXPositivelyFaster || isXNegativelyFaster || isYPositivelyFaster || isYNegativelyFaster)
        {
            targetZoom = 8;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }
        else
        {
            targetZoom = 5;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }

        Vector3 endPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, endPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}
