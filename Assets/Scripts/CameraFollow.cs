using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    public BoxCollider2D zone;
    public float followSpeed = 1.0f;
    public float minY = -4.0f;
    public float yOffset = -1.5f;
    Vector3 oldPosition;
    Vector2 size;
    
    Vector2 GetCameraSize() {
        return new Vector2(
            Camera.main.orthographicSize * Screen.width / Screen.height,
            Camera.main.orthographicSize
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        size = GetCameraSize(); 
        print(size);
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = target.transform.position;
        if (target2 != null) {
            desiredPosition = Vector3.Slerp(desiredPosition, target2.transform.position, 0.5f);
        }
        if (zone != null) {
            desiredPosition = new Vector3(
                Mathf.Min(zone.bounds.max.x - size.x, Mathf.Max(zone.bounds.min.x + size.x, desiredPosition.x)),
                Mathf.Min(zone.bounds.max.y - size.y, Mathf.Max(zone.bounds.min.y + size.y, desiredPosition.y)),
                desiredPosition.z
            );
        }
        oldPosition = new Vector3(oldPosition.x, Mathf.Max(oldPosition.y, minY), oldPosition.z);
        oldPosition = Vector3.Slerp(oldPosition, desiredPosition, Time.deltaTime * followSpeed);
        transform.position = new Vector3(
            oldPosition.x,
            oldPosition.y,
            -10.0f
        );
    }
}
