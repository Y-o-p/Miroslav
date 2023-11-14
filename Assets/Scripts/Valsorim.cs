using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Valsorim : MonoBehaviour
{
    public GameObject arrowPrefab;
    Animator animator;
    public float test = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Phase1");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetTrigger("Phase1");
    }

    void FireArrow(Vector3 position, float rotation, float force) {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.force = force;
        arrow.transform.position = position;
        arrow.transform.Rotate(new Vector3(0, 0, rotation));
    }

    void FireRandomArrow() {
        float randomX = Random.Range(0, 5);
        for (int i = 0; i < 3; i++) {
            Vector3 arrowPosition = new Vector3(transform.position.x + randomX + i, transform.position.y + 5.0f, transform.position.z);
            FireArrow(arrowPosition, -90.0f, 10.0f);
        }
    }
}
