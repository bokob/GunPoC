using UnityEngine;

public class Ammo : MonoBehaviour
{
    public void SetShoot()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}