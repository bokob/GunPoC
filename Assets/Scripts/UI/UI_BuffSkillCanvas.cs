using UnityEngine;

public class UI_BuffSkillCanvas : MonoBehaviour
{
    Transform _buffSkillLayoutGroupTransform;
    [SerializeField] UI_BuffElement _buffPrefab;

    void Awake()
    {
        _buffSkillLayoutGroupTransform = transform.GetChild(0);
    }

    void Start()
    {
        UI_InGameEventBus.OnAddBuffAction += AddBuff;
        UI_InGameEventBus.OnChangeBuffActiveTimeAction += UpdateBuffElement;
    }

    void Update()
    {
        // 테스트 코드
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(_buffPrefab, _buffSkillLayoutGroupTransform);
        }
    }

    public void AddBuff(Buff buff)
    {
        UI_BuffElement ui_buffElement = Instantiate(_buffPrefab, _buffSkillLayoutGroupTransform);
        ui_buffElement.Init(buff);
    }

    public void UpdateBuffElement()
    {
        foreach(Transform buffElement in _buffSkillLayoutGroupTransform)
        {
            buffElement.GetComponent<UI_BuffElement>().UpdateBuffState();
        }
    }

    void OnDestroy()
    {
        UI_InGameEventBus.OnAddBuffAction = null;
        UI_InGameEventBus.OnChangeBuffActiveTimeAction = null;
    }
}