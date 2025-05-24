using UnityEngine;
using UnityEngine.UI;

public class UI_StopCanvas : MonoBehaviour
{
    Canvas _cavnas;

    void Awake()
    {
        _cavnas = GetComponent<Canvas>();
        _cavnas.enabled = false;
    }

    void Start()
    {
        UI_InGameEventBus.OnPauseAction += ToggleCanvas;
    }

    public void ToggleCanvas()
    {
        _cavnas.enabled = GameManager.Instance.IsPause; // 게임이 정지 상태가 아닐 때만 캔버스 활성화
    }

    private void OnDestroy()
    {
        UI_InGameEventBus.OnPauseAction -= ToggleCanvas;
    }
}