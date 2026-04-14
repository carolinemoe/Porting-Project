using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerController>() != null)
        {
            Destroy(gameObject);
        }
    }
}