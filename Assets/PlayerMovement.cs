using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D player;
    private BoxCollider2D playerCollision;
    [SerializeField] private LayerMask jumpableGround;
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(xDirection * 7f, player.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, 10f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerCollision.bounds.center, playerCollision.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
