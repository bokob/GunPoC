using UnityEngine;

public class Enemy : MonoBehaviour
{
    int MaxHP = 3;
    [field: SerializeField] public int HP { get; private set; } = 3;

    bool _isDead = false;

    [SerializeField] GameObject _spiritPrefab;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(int damage = 1)
    {
        if (_isDead)
            return;

        HP = Mathf.Clamp(HP - damage, 0, MaxHP);
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _isDead = true;
        Instantiate(_spiritPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}