using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 finishPos = Vector3.zero;
    public float speed = 0.5f;

    private Vector3 startPos;

    // how far along the "track" between start and finish
    private float trackPercent = 0;

    // current movement direction
    private int direction = 1;
        
    void Start()
    {
        // placement in the scene is the position to move from
        startPos = transform.position;    
    }
        
    void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x, y, startPos.z);

        // change direction at both start and end
        if ((direction == 1 && trackPercent > .9f) || (direction == -1 && trackPercent < .1f)){
            direction *= -1;
        }
    }
}
