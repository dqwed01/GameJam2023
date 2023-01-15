using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D player;
    private BoxCollider2D playerCollision;
    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    private float xDirection;
    [SerializeField] private LayerMask jumpableGround;

    private enum playerMoveStates { idle, running, jumping, falling };

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(xDirection * 7f, player.velocity.y);

        if (Input.GetButton("Jump") && IsGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, 10f);
            player.GetComponent<Animator>().SetInteger("state", 2);
        }

        handlePlayerAnimation();
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerCollision.bounds.center, playerCollision.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void handlePlayerAnimation()
    {
        playerMoveStates setValue = playerMoveStates.idle;
        setValue = handleRunning(setValue);
        setValue = handleJumping(setValue);
        setValue = handleFalling(setValue);
        playerAnimator.SetInteger("state", (int)setValue);
    }

    private playerMoveStates handleRunning(playerMoveStates setValue)
    {
        if (xDirection == 0) return setValue;
        if (xDirection < 0) playerSprite.flipX = true;
        else if (xDirection > 0) playerSprite.flipX = false;
        return playerMoveStates.running;
    }

    private playerMoveStates handleJumping(playerMoveStates setValue)
    {
        if (player.velocity.y > .1f)
        {
            if (player.velocity.x > 0) playerSprite.flipX = false;
            else if (player.velocity.x < 0) playerSprite.flipX = true;
            return playerMoveStates.jumping;
        }
        else return setValue;
    }

    private playerMoveStates handleFalling(playerMoveStates setValue)
    {
        if (player.velocity.y < -.1f)
        {
            if (player.velocity.x > 0) playerSprite.flipX = false;
            else if (player.velocity.x < 0) playerSprite.flipX = true;
            return playerMoveStates.falling;
        }
        else return setValue;
    }
}
