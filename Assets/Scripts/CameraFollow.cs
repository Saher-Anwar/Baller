using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 endPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, endPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
