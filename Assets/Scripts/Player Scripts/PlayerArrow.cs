using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
