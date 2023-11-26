using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Valsorim : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject player;
    Animator animator;
    public float test = 0.0f;
    string position_state = "Left";
    Dictionary<string, string[]> directions;

    string GetNewPositionState(string direction) {
        return direction.Split(new string[] {"To"}, StringSplitOptions.None)[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        directions = new Dictionary<string, string[]>();
        directions.Add("Left", new string[] {"LeftToCenter"});
        directions.Add("Center", new string[] {"CenterToLeft", "CenterToRight"});
        directions.Add("Right", new string[] {"RightToCenter"});

        animator = GetComponent<Animator>();
        animator.SetTrigger("Phase1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            animator.SetTrigger("CenterToRight");
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            animator.SetTrigger("CenterToLeft");
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            animator.SetTrigger("LeftToCenter");
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            animator.SetTrigger("RightToCenter");
        }

        AnimatorClipInfo[] current_clip = animator.GetCurrentAnimatorClipInfo(0);
        string clip_name = current_clip[0].clip.name;
    }

    void RandomAction() {
        int action = UnityEngine.Random.Range(0, 2);
        if (action == 0) {
            MoveRandom();
        }
        else if (action == 1) {
            AttackRandom();
        }
    }

    void AttackRandom() {
        int attack = UnityEngine.Random.Range(0, 2);
        if (attack == 0) {
            animator.SetTrigger("Sniper");
        }
        else if (attack == 1) {
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
        float randomX = UnityEngine.Random.Range(-10, 10);
        for (int i = 0; i < 3; i++) {
            Vector3 arrowPosition = new Vector3(transform.position.x + randomX + i, transform.position.y + 5.0f, transform.position.z);
            FireArrow(arrowPosition, -90.0f, 10.0f);
        }
    }

    void FireAtPlayer() {
        FireArrow(transform.position, Vector3.Angle(transform.position, player.transform.position), 200.0f);
    }
}
