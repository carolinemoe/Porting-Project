using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float jumpForce = 10f;

    private Animator anim;
    private SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
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
}

