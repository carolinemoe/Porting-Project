using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPos;
    private bool movingRight = false;

    private SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
        SetDirection();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
                SetDirection();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
                SetDirection();
            }
        }
    }

    void SetDirection()
    {
        if (movingRight)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}