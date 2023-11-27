using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public float health;
    float speed = 1.5f;
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

    public string newTag = "Untagged";

    // Start is called before the first frame update
    void Start()
    {
        dead = 0;
        this.animator = GetComponent<Animator>();
        check_name();
    }


    // Update is called once per frame
    void Update()
    {
        if (dead == 0)
        {
            if (isOrc)
            {
                Vector3 pos = transform.position;
                pos.x += speed/2f * Time.deltaTime;
                transform.position = pos;
                transform.localScale = new Vector3(speed, 2, 2);
            }
            else
            {
                Vector3 pos = transform.position;
                pos.x += speed*0.66f * Time.deltaTime;
                transform.position = pos;
                transform.localScale = new Vector3(speed, 1.5f, 1.5f);
            }
        }
    }

    private void check_name()
    {
        if (gameObject.name.Contains("bslimePrefab"))
        {
            // Alien 1 - Use moveSpeed
            health = 10;
            isblue = true;
        }
        else if (gameObject.name.Contains("rslimePrefab"))
        {
            // Alien 2 - Use moveSpeed2
            health = 15;
            isred = true;
        }
        else if (gameObject.name.Contains("morbPrefab"))
        {
            // Alien 2 - Use moveSpeed2
            health = 20;
            isOrc = true;
            speed = 2f;
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
                gameObject.tag = newTag;
                gameObject.layer = 7;

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

