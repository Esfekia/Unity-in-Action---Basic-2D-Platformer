using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;

    private Rigidbody2D body;
        
    // Start is called before the first frame update
    void Start()
    {
        // need this other component attached to this GameObject
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // set only the horizontal movement, preserve existing vertical movement
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
}
