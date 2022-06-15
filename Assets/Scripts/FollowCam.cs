using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform target;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

  
    void LateUpdate()
    {
        // preserve the Z position while constantly updating the X and Y depending on the player
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // smooth transition from current to target position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
