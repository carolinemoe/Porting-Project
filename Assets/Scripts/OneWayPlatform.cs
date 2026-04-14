using UnityEngine;

public class OneWayPlatformHelper : MonoBehaviour
{
    public Collider2D playerCollider;
    public Rigidbody2D rb;
    public LayerMask oneWayPlatformLayer;

    public Transform feetPoint;
    public Vector2 checkSize = new Vector2(0.3f, 0.1f);

    private Collider2D currentPlatform;

    void Update()
    {
        CheckPlatform();

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down key pressed, trying to drop through platform");
            if (currentPlatform != null)
            {
                Debug.Log("Dropping through platform: " + currentPlatform.name);
                StartCoroutine(DisableCollisionTemporarily(currentPlatform, 0.25f));
            }
            else
            {
                Debug.Log("No platform detected to drop through");
            }
        }
    }

    void CheckPlatform()
    {
        Vector2 boxCenter = new Vector2(
            playerCollider.bounds.center.x,
            playerCollider.bounds.min.y - 0.05f
        );

        Vector2 boxSize = new Vector2(
            playerCollider.bounds.size.x * 0.9f,
            0.2f
        );

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, oneWayPlatformLayer);

        if (hit == null)
        {
            currentPlatform = null;
            return;
        }

        currentPlatform = hit;
    }

    System.Collections.IEnumerator DisableCollisionTemporarily(Collider2D platform, float time)
    {
        Physics2D.IgnoreCollision(playerCollider, platform, true);
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreCollision(playerCollider, platform, false);
    }

    private void OnDrawGizmosSelected()
    {
        if (feetPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(feetPoint.position, checkSize);
        }
    }
}