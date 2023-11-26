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

    private bool isOrc=false;
    private bool isred = false;
    private bool isblue = false;

    public AudioSource red_death_sound;
    public AudioSource blue_death_sound;
    public AudioSource orc_death_sound;

    // Start is called before the first frame update
    void Start()
    {
        health = 15;
        dead = 0;
        this.animator = GetComponent<Animator>();
        check_name();
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

    private void check_name()
    {
        if (gameObject.name==("bslimePrefab"))
        {
            // Alien 1 - Use moveSpeed
            isblue = true;
        }
        else if (gameObject.name==("rslimePrefab"))
        {
            // Alien 2 - Use moveSpeed2
            isred = true;
        }
        else if (gameObject.name==("morbPrefab"))
        {
            // Alien 2 - Use moveSpeed2
            isOrc = true;
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

                if (isblue)
                {
                    blue_death_sound.Play();

                }
                else if (isred)
                {
                    red_death_sound.Play();
                }
                else if (isOrc)
                {
                    orc_death_sound.Play();
                }
            }
        }
    }
}

