using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: SerializeField] public bool IsDead { get; protected set; } = false;   // 사망 여부
    [field: SerializeField] public bool IsHit { get; protected set; } = false;    // 피격 여부
    [field: SerializeField] public float MaxHP { get; protected set; }            // 최대 체력
    [field: SerializeField] public float HP { get; protected set; }

    //[field: SerializeField] public bool IsDash { get; private set; }            // 대시 여부

    WaitForSeconds _hitWait = new WaitForSeconds(0.5f);

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        StartCoroutine(HitCoroutine());
        Debug.Log($"데미지: {damage}");
        HP = Mathf.Clamp(HP - damage, 0, MaxHP);

        if (HP <= 0)
        {
            // 사망 처리
            IsDead = true;
            Debug.Log("사망");
        }
    }

    IEnumerator HitCoroutine()
    {
        IsHit = true;
        yield return _hitWait;
        IsHit = false;
    }
}