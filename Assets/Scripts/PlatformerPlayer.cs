using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;

    private Rigidbody2D body;

    private Animator anim;
        
    // Start is called before the first frame update
    void Start()
    {
        // need this other component attached to this GameObject
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // set only the horizontal movement, preserve existing vertical movement
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        // speed is greater than zero even if velocity is negative
        anim.SetFloat("speed", Mathf.Abs(deltaX));

        // floats aren't always exact, so compare using Approximately()
        if (!Mathf.Approximately(deltaX, 0))
        {
            // when moving, scale positive or negative 1 to face right or left
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
    }
}
