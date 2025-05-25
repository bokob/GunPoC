using UnityEngine;

public class Area : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ammo ammo))
        {
            Destroy(ammo.gameObject);
        }
    }
}