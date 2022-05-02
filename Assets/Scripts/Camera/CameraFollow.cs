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

    public int maxCameraDistance = 24;
    public int minCameraDistance = 10;

    private UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera ppc;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        target = GameObject.Find("Player").transform;
        
        ppc = GetComponent<UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera>();
        ppc.assetsPPU = PlayerInfo.cameraDistance;

    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Equals) && PlayerInfo.cameraDistance < maxCameraDistance) {
            PlayerInfo.cameraDistance+=2;
            ppc.assetsPPU = PlayerInfo.cameraDistance;
        }

        if(Input.GetKeyDown(KeyCode.Minus) && PlayerInfo.cameraDistance > minCameraDistance) {
            PlayerInfo.cameraDistance-=2;
            ppc.assetsPPU = PlayerInfo.cameraDistance;
        }
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
