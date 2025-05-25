using System;
using System.Reflection;
using UnityEngine;

public enum BuffType
{
    Fury,   // 빨간색 - 공격력 증가
    Sad,    // 파란색 - 이동 속도 증가
    Joy,    // 초록색 - 회복 속도 증가
    Crazy,  // 보라색 - 공격 속도 증가
}

[Serializable]
public class Buff : MonoBehaviour
{
    public BuffSO buff;             // 스킬 정보가 담긴 ScriptableObject
    public int stackCount = 0;      // 버프 중첩 횟수
    public float _activeTime = 0f;  // 버프가 활성화된 시간
    public float _elapsedTime = 0f; // 버프가 적용된 시간
    public float offsetValue = 0f;

    public BuffType BuffType; // 버프 타입

    Action action;

    bool isStop = false;

    object PlayerStatusObject;

    public UI_BuffElement ui_BuffElement;

    void Start()
    {
        _elapsedTime = 0f;
        offsetValue = 0f;
    }

    void Update()
    {
        if (isStop)
        {
            FieldInfo fieldInfo = PlayerStatusObject.GetType().GetTypeInfo().GetDeclaredField(buff.statName);
            fieldInfo.SetValue(PlayerStatusObject, 0);
            stackCount = 0;
            offsetValue = 0;
            Destroy(ui_BuffElement.gameObject);
            action?.Invoke();
            return;
        }

        if (_elapsedTime >= _activeTime)
        {
            isStop = true;
        }
        else
        {
            _elapsedTime += Time.deltaTime;
            Debug.LogWarning($"현재 버프 적용 시간: {_elapsedTime} / {_activeTime}초");
            ui_BuffElement?.SetSliderValue(_elapsedTime);
        }
    }

    /// <summary>
    /// 버프 적용
    /// </summary>
    /// <param name="obj">버프가 적용될 객체</param>
    /// <param name="finishDone"> 버프가 끝났을 때 실행할 것 </param>
    public void Activated(object obj, Action finishDone)
    {
        if(stackCount == 0) // 처음
        {
            PlayerStatusObject = obj;
            stackCount = 1;
            _activeTime = buff.duration;
            offsetValue = buff.effectValue;
            action = finishDone;
            UI_InGameEventBus.OnAddBuffAction?.Invoke(this);
        }
        else // 중첩
        {
            stackCount++;
            _activeTime = buff.duration * stackCount;
            offsetValue = buff.effectValue * stackCount;
        }

        UI_InGameEventBus.OnChangeBuffActiveTimeAction?.Invoke();
        FieldInfo fieldInfo = obj.GetType().GetTypeInfo().GetDeclaredField(buff.statName);
        fieldInfo.SetValue(obj, offsetValue);
    }
}