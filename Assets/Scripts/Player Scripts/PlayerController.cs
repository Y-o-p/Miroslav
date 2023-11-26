using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    SpriteRenderer sprite;
    Animator animator;
    float speed = 5.0f; //player speed
    bool isGrounded; //check if player is grounded

    public GameObject arrowPrefab;
    private GameObject arrow;

    public AudioSource jump_sound;
    void Awake()
    {
        //get player rigidbody and animator
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (this != null)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            //player movement left and right
            playerRigidbody.velocity = new Vector2(horizontalInput * speed, playerRigidbody.velocity.y);

            //flip player sprite when moving left and right
            if (horizontalInput > 0.01f)
            {
                sprite.flipX = false;
            }
            else if (horizontalInput < -0.01f)
            {
                sprite.flipX = true;
            }

            //player jump
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                Jump();
            }

            // Shoot an arrow if the spacebar is pressed 
            if ((Input.GetKeyDown(KeyCode.Space)))
            {
                animator.SetTrigger("shootTrigger");
                Invoke("SpawnArrow", 0.5f);
            }

            animator.SetBool("isRunning", horizontalInput != 0); //check if player is running
            animator.SetBool("isGrounded", isGrounded); //check if player is grounded

            if (transform.position.y < -9.0f)
            {
                GameManager.instance.player_die();
                // Perform actions when the player is below 9 in the y value
                Destroy(gameObject);

            }
        }
    } 

    //player jump
    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        isGrounded = false;
        jump_sound.Play();
        animator.SetTrigger("jumpTrigger");
    }

    //spawn arrow
    void SpawnArrow()
    {
        //create arrow
        arrow = Instantiate<GameObject>(
            arrowPrefab,
            new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), 
            Quaternion.identity
        );

        //set arrow direction
        Vector2 arrowDirection = !sprite.flipX ? Vector2.right : Vector2.left;
        arrow.GetComponent<PlayerArrow>().Initialize(arrowDirection);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            //the isGrounded flag is how we keep track of if the player is jumping or not.
            //it is based on whether they are in contact with a game object tagged as "Ground".
            //when you jump, we set isGrounded to false, and when you land, we set it to true.
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("hurtTrigger");
        }
        else if (collision.gameObject.CompareTag("EProjectile"))
        {
            animator.SetTrigger("hurtTrigger");
            Destroy(collision.gameObject);
        }
    }
}  