using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 1.0f;
    public float minY = -4.0f;
    public float yOffset = -1.5f;
    Vector3 oldPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        oldPosition = Vector3.Slerp(oldPosition, target.transform.position, Time.deltaTime * followSpeed);
        transform.position = new Vector3(
            oldPosition.x,
            Mathf.Max(oldPosition.y + yOffset, minY),
            -10.0f
        );
    }
}
