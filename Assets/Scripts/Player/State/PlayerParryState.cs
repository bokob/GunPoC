using System;
using System.Collections;
using UnityEngine;

public class PlayerParryState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    [SerializeField] bool _isBlock = false;          // 막기 상태 여부
    [SerializeField] bool _isParryWindow = false;    // 패링 판정 시간에 들어왔는지 여부
    [SerializeField] float _parryWindow = 0.2f;      // 패링 윈도우 시간 (판정 시간)
    float _knockBackForce = 5f;                      // 넉백 힘

    Rigidbody2D _rb;
    Coroutine _blockCoroutine;
    PlayerStatus _playerStatus;
    //Shouting _shouting;
    //PlayerText _playerText;

    void Start()
    {
        _playerFSM = GetComponent<PlayerFSM>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerStatus = GetComponent<PlayerStatus>();
        //_shouting = GetComponentInChildren<Shouting>();
        //_playerText = GetComponentInChildren<PlayerText>();
    }

    public override void OnStateEnter() 
    {
        Debug.Log("Parry 진입");
        StartBlockCoroutine();
    }

    public override void OnStateUpdate() 
    {
        if(!Managers.Input.IsPressParry)
            _playerFSM.SetState(PlayerState.Idle); // 패링 버튼을 떼면 Idle 상태로 전환
    }

    public override void OnStateExit() 
    {
        StopBlock();
    }


    #region 막기
    public void StartBlockCoroutine()
    {
        //if (_isBlock)
        //    return;
        if (_blockCoroutine != null)
            StopCoroutine(_blockCoroutine);

        _blockCoroutine = StartCoroutine(BlockCoroutine());
    }

    IEnumerator BlockCoroutine()
    {
        //_anim.SetTrigger("AttackTrigger");
        _isBlock = true;
        _isParryWindow = true;
        yield return new WaitForSeconds(_parryWindow);
        _isParryWindow = false;

        // 막기는 계속 유지
        _blockCoroutine = null;
    }

    // 막기 해제
    public void StopBlock()
    {
        _isBlock = false;
    }

    void TryBlock(IParryable attacker)
    {
        if (attacker.ParryType == ParryType.Projectile)
            Destroy(attacker.ParriedTransform.gameObject);
        else
            Debug.Log("근접 공격 막음");
    }
    #endregion

    #region 패링
    void TryParry(IParryable attacker)
    {
        //_shouting.Play();
        //_playerText.StartShowTextCoroutine(0);
        attacker.Parried(transform);
    }
    #endregion

    // 공격 받는 함수 (적이 호출)
    public void ReceiveAttack(IParryable attacker)
    {
        if (_isBlock)
        {
            Action<IParryable> parryOrBlock = (_isParryWindow) ? TryParry : TryBlock;
            parryOrBlock?.Invoke(attacker);
        }
        else // 못 막은 경우 데미지 입고 넉백
        {
            //_playerText.StartShowTextCoroutine(1);
            _playerStatus.TakeDamage(5);
            KnockBack(attacker);
        }
    }

    public void KnockBack(IParryable attacker)
    {
        Vector2 knockBack = (transform.position - attacker.ParriedTransform.position).normalized * _knockBackForce;
        _rb.AddForce(knockBack, ForceMode2D.Impulse);
    }
}
