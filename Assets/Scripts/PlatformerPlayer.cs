using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private BoxCollider2D box;

    private Animator anim;
        
    // Start is called before the first frame update
    void Start()
    {
        // need this other component attached to this GameObject
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // get this component to use the player's collider as an area to check
        box = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {


        // set only the horizontal movement, preserve existing vertical movement
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        // check below the collider's min Y values
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }
        // check both on ground and not moving
        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;
        if (hit != null)
        {
            // check whether the platform under the player is a moving platform
            platform = hit.GetComponent<MovingPlatform>();
        }
        
        // either attach to the platform or clear transform.parent.
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }
            

        // speed is greater than zero even if velocity is negative
        anim.SetFloat("speed", Mathf.Abs(deltaX));

        // default scale 1 if not on moving platform
        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }

        // floats aren't always exact, so compare using Approximately()
        if (!Mathf.Approximately(deltaX, 0))
        {
            // when moving, scale positive or negative 1 to face right or left
            transform.localScale = new Vector3(Mathf.Sign(deltaX) /pScale.x, 1/pScale.y, 1);
        }
    }
}
