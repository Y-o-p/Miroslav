using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator animator;
    float speed = 5.0f; //player speed
    bool isGrounded; //check if player is grounded

    public GameObject arrowPrefab;
    private GameObject arrow;

    void Awake()
    {
        //get player rigidbody and animator
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        //player movement left and right
        playerRigidbody.velocity = new Vector2(horizontalInput * speed, playerRigidbody.velocity.y);

        //flip player sprite when moving left and right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //player jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        // Shoot an arrow if the spacebar is pressed 
        if ((Input.GetKeyDown(KeyCode.Space))) {
            animator.SetTrigger("shootTrigger");
            Invoke("SpawnArrow", 0.5f);
        }

        animator.SetBool("isRunning", horizontalInput != 0); //check if player is running
        animator.SetBool("isGrounded", isGrounded); //check if player is grounded
    } 

    //player jump
    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        isGrounded = false;
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
        Vector2 arrowDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
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
    }
}  