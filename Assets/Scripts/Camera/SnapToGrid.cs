using UnityEngine;
using System.Collections;

// This script is executed in the editor
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
#if UNITY_EDITOR
    public bool snapToGrid = true;
    public float snapValue = 0.50f;

    private Vector3 lastPosition;

    // Adjust size and position
    void LateUpdate ()
    {   
        if (snapToGrid && transform.position != lastPosition)
            this.transform.position = new Vector3(Mathf.Floor(transform.position.x) + snapValue, Mathf.Floor(transform.position.y) + snapValue, transform.position.z);
            lastPosition = transform.position;
        }
#endif
}