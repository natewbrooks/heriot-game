using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    private Vector3 offset = new Vector3(0f,0f,-10f);
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null) {
            Vector3 targetPos = target.position + offset;
            cam.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
    }
}
