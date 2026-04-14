using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public float knockbackForceX = 5f;
    public float knockbackForceY = 5f;
    public int maxJumps = 2;

    private Animator anim;
    private SpriteRenderer sprite;

    private bool IsHurt = false;
    private int jumpCount = 0;
    private bool canDoubleJump = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsHurt)
        {
            float move = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

            if (move < 0)
            {
                sprite.flipX = true;
            }
            else if (move > 0)
            {
                sprite.flipX = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (jumpCount < maxJumps)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    jumpCount++;
                }
            }

            UpdateAnimationState();
        }
    }

    private void UpdateAnimationState()
    {
        if (IsHurt) return;

        if (rb.linearVelocity.y > 0.1f)
        {
            anim.Play("Jump");
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            anim.Play("Fall");
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            anim.Play("Run");
            sprite.flipX = rb.linearVelocity.x < 0;
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    Destroy(collision.gameObject);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                    return;
                }
            }
            Hurt(collision.transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("OneWayPlatform"))
        {
            jumpCount = 0;
        }
    }

    void Hurt(Transform enemy)
    {
        if (IsHurt) return;

        IsHurt = true;
        anim.SetTrigger("IsHurt");

        float direction = transform.position.x < enemy.position.x ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * knockbackForceX, knockbackForceY);

        Invoke(nameof(ResetHurt), 0.5f);
    }


    void ResetHurt()
    {
        IsHurt = false;
    }
}

