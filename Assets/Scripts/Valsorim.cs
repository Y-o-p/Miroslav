using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Valsorim : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject player;
    public Collider2D zone;
    public Sprite defaultSprite;
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer sprite;
    public float test = 0.0f;
    string position_state = "Right";
    string next_direction = "Left";
    Dictionary<string, string[]> directions;
    int attacks = 0;
    public int health = 30;

    string GetNewPositionState(string direction) {
        return direction.Split(new string[] {"To"}, StringSplitOptions.None)[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        directions = new Dictionary<string, string[]>();
        directions.Add("Left", new string[] {"LeftToCenter"});
        directions.Add("Center", new string[] {"CenterToLeft", "CenterToRight"});
        directions.Add("Right", new string[] {"RightToCenter"});

        animator = GetComponent<Animator>();
        animator.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.is_winner) {
            AnimatorClipInfo[] current_clip = animator.GetCurrentAnimatorClipInfo(0);
            string clip_name = current_clip[0].clip.name;

            if (player.transform.position.x < transform.position.x) {
                sprite.flipX = true;
            }
            else {
                sprite.flipX = false;
            }

            if (health <= 0) {
                Death();
            }
        }
    }

    public void Death() {
        GameManager.is_winner = true;
        body.bodyType = RigidbodyType2D.Dynamic;
        animator.Play("Death");
    }

    void RandomAction() {
        if (attacks == 0) {
            attacks = UnityEngine.Random.Range(3, 6);
            MoveRandom();
        }
        else {
            attacks--;
            AttackRandom();
        }
        Debug.Log(attacks);
    }

    void AttackRandom() {
        float attack = UnityEngine.Random.Range(0.0f, 1.0f);
        print("Attack: " + attack);
        if (attack < 0.75) {
            animator.SetTrigger("Sniper");
        }
        else {
            animator.SetTrigger("TripleRainArrow");
        }
    }

    void MoveRandom() {
        string direction = "";
        if (position_state == "Right") {
            direction = "RightToCenter";
        }
        else if (position_state == "Left") {
            direction = "LeftToCenter";
        }
        else if (position_state == "Center") {
            if (next_direction == "Left") {
                next_direction = "Right";
                direction = "CenterToLeft";
            }
            else {
                next_direction = "Left";
                direction = "CenterToRight";
            }
        }
        animator.SetTrigger(direction);
        position_state = GetNewPositionState(direction);
    }

    void FireArrow(Vector3 position, float rotation, float force) {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.force = force;
        arrow.transform.position = position;
        arrow.transform.Rotate(new Vector3(0, 0, rotation));
    }

    void FireRandomArrow() {
        for (int i = -1; i < 2; i++) {
            Vector3 arrowPosition = new Vector3(player.transform.position.x + i, zone.bounds.max.y, transform.position.z);
            FireArrow(arrowPosition, -90.0f, 10.0f);
        }
    }

    void FireAtPlayer() {
        Vector3 dist = player.transform.position + new Vector3(0, 0.5f) - transform.position;
        float angle = Vector2.Angle(Vector2.right, dist);
        if (dist.y < 0) {
            angle = -angle;
        }
        FireArrow(transform.position, angle, 300.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PArrow" && health > 0) {
            health -= 1;
            print("heatlh of the boss:" + health);
        }
    }
}
