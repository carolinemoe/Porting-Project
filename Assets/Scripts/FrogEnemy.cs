using System.Collections;
using UnityEngine;

public class FrogEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;

    public float jumpForceX = 2f;
    public float jumpForceY = 5f;
    public float waitTime = 1.5f;

    private bool isGrounded = true;
    private bool movingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(JumpLoop());
    }

    void Update()
    {
        if (isGrounded)
        {
            anim.Play("FrogIdle");
        }
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => isGrounded);
            yield return new WaitForSeconds(waitTime);
        
            Jump();           
        }
    }

    void Jump()
    {
        isGrounded = false;

        float direction = movingRight ? 1f : -1f;

        sprite.flipX = direction < 0;

        rb.linearVelocity = new Vector2(direction * jumpForceX, jumpForceY);

        anim.Play("FrogJump");

        movingRight = !movingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (rb.linearVelocity.y <= 0)
            {
                isGrounded = true;
            }
        }
    }
}