using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(Mathf.SmoothStep(cam.transform.position.x, target.position.x, .09f), Mathf.SmoothStep(cam.transform.position.y, target.position.y, .09f), cam.transform.position.z);
    }
}
