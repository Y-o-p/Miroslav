using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    float health;
    float speed = 1f;
    Rigidbody2D rigid2D;
    Animator animator;
    float chanceToChangeDirections = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        health = 15;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

    }

    void FixedUpdate()
    {

        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedWith = other.gameObject;
        if (other.tag == "PArrow")
        {
            Destroy(collidedWith);
            health = health - 5;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
