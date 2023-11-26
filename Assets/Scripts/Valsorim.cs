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
    Animator animator;
    SpriteRenderer sprite;
    public float test = 0.0f;
    string position_state = "Right";
    Dictionary<string, string[]> directions;
    int attacks = 0;

    string GetNewPositionState(string direction) {
        return direction.Split(new string[] {"To"}, StringSplitOptions.None)[1];
    }

    // Start is called before the first frame update
    void Start()
    {
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
        AnimatorClipInfo[] current_clip = animator.GetCurrentAnimatorClipInfo(0);
        string clip_name = current_clip[0].clip.name;

        if (player.transform.position.x < transform.position.x) {
            sprite.flipX = true;
        }
        else {
            sprite.flipX = false;
        }
    }

    void Wake() {
        sprite.sprite = defaultSprite;
        sprite.color = new Color(0.1f, 0.1f, 0.1f);
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
        string[] movements_possible = directions[position_state];
        string direction = movements_possible[UnityEngine.Random.Range(0, movements_possible.Length)];
        animator.SetTrigger(direction);
        position_state = GetNewPositionState(direction);
        print(position_state);
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
        Vector3 dist = player.transform.position - transform.position;
        float angle = Vector2.Angle(Vector2.right, dist);
        if (dist.y < 0) {
            angle = -angle;
        }
        FireArrow(transform.position, angle, 300.0f);
    }
}
