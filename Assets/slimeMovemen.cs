using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class slimeMovemen : MonoBehaviour
{
    private Rigidbody2D slime;
    private BoxCollider2D slimeCollision;
    private SpriteRenderer slimeSprite;
    private Animator slimeAnimator;
    public float slimeSpeed;
    [SerializeField] private LayerMask collidableFace;

    // Start is called before the first frame update
    void Start()
    {
        slime = GetComponent<Rigidbody2D>();
        slimeCollision = GetComponent<BoxCollider2D>();
        slimeSprite = GetComponent<SpriteRenderer>();
        slimeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (inContact()) slimeSpeed *= -1;
        slime.velocity = new Vector2(slimeSpeed, slime.velocity.y);
    }

    private bool inContact()
    {
        if(Physics2D.BoxCast(slimeCollision.bounds.center, slimeCollision.bounds.size, 0f, Vector2.left, .1f, collidableFace)) return true;
        if (Physics2D.BoxCast(slimeCollision.bounds.center, slimeCollision.bounds.size, 0f, Vector2.right, .1f, collidableFace)) return true;
        return false;
    }
}
