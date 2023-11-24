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
    float dead;
    float delta;
    // Start is called before the first frame update
    void Start()
    {
        health = 15;
        dead = 0;
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == 0)
        {
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;
            transform.localScale = new Vector3(speed, 1, 1);
        }
    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections && dead == 0)
        {
            speed *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedWith = other.gameObject;
        if (other.tag == "PArrow" && dead == 0)
        {
            Destroy(collidedWith);
            health = health - 5;
            if (health <= 0)
            {
                dead = 1;
                this.animator.SetTrigger("dTrigger");
            }
        }
    }
}

