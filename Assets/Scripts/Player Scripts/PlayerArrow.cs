using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public float speed = 10f; //arrow speed

    private void Update()
    {
        //move arrow
        transform.position += speed * Time.deltaTime * transform.right; 
    }

    //destroy arrow when it leaves the screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
