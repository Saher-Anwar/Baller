using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
