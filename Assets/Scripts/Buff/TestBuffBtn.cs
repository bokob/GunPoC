using UnityEngine;
using UnityEngine.UI;

public class TestBuffBtn : MonoBehaviour
{
    Button _btn;

    // 버프 프리팹
    public Transform buffPrefab;
    public Spirit spiritPrefab;

    //// Player 스크립트
    //public PlayerStatus playerStatus;

    void Start()
    {
        //// PlayerStatus 컴포넌트 찾기
        //playerStatus = FindAnyObjectByType<PlayerStatus>();

        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClicked);
    }

    public void OnClicked()
    {
        Spirit spirit = Instantiate(spiritPrefab);
        BuffType buffType = buffPrefab.GetComponent<Buff>().BuffType;
        spirit.SetSpirit(buffType);

        //// 생성된 버프 객체를 player에 추가
        //playerStatus.AddBuff(buffType);
    }
}