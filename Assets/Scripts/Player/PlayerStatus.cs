using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: SerializeField] public bool IsDead { get; protected set; } = false;   // 사망 여부
    [field: SerializeField] public bool IsHit { get; protected set; } = false;    // 피격 여부
    [field: SerializeField] public float MaxHP { get; protected set; }            // 최대 체력
    [field: SerializeField] public float HP { get; protected set; }

    //[field: SerializeField] public bool IsDash { get; private set; }            // 대시 여부

    public float AttackPower => defaultAttackPower + attackPower;
    public float MoveSpeed => defaultMoveSpeed + moveSpeed;
    
    public float AttackSpeed => defaultAttackSpeed + attackSpeed;
    public float HealSpeed => defaultHealSpeed + healSpeed;

    WaitForSeconds _hitWait = new WaitForSeconds(0.5f);

    [Header("버프 관련")]
    public List<Buff> _buffList = new List<Buff>();
    public List<BuffType> _buffTypeList = new List<BuffType>();

    public float defaultAttackPower = 1f;
    public float defaultAttackSpeed =1f;
    public float defaultHealSpeed = 1f;
    public float defaultMoveSpeed = 2f;

    public float attackPower;
    public float attackSpeed;
    public float healSpeed;
    public float moveSpeed;

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

    public void Drain()
    {

    }

    // 버프 추가
    public void AddBuff(BuffType buffType)
    {
        if (!_buffTypeList.Contains(buffType)) // 존재하지 않은 경우
        {
            // 버프 추가
            _buffTypeList.Add(buffType);
            var buffInstance = Managers.Resource.Instantiate($"Buffs/{buffType}", transform);
            Buff buff = buffInstance.GetComponent<Buff>();
            _buffList.Add(buff);
            buff.Activated(this, () =>
            {
                // 버프가 끝났을 때
                _buffTypeList.Remove(buffType);
                _buffList.Remove(buff);
                Destroy(buffInstance.gameObject);
            });
        }
        else // 존재하는 경우
        {
            // 버프 찾아서 중첩시키기
            Buff buff = _buffList.Find(buffElement => buffElement.BuffType == buffType);
            buff.Activated(this, () => { });
        }
    }

    void PrintStats()
    {
        print($"공격력: {defaultAttackPower} + {attackPower} = {AttackPower}");
        print($"공격 속도: {defaultAttackSpeed} + {attackSpeed} = {AttackSpeed}");
        print($"이동 속도: {defaultHealSpeed} + {healSpeed} = {HealSpeed}");
        print($"재생 속도: {defaultMoveSpeed} + {moveSpeed} = {MoveSpeed}");
    }
}