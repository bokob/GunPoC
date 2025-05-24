using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;

    public bool IsPause { get; set; } = false;

    public Transform PlayerTransform { get; private set; }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void Init()
    {
        TogglePause();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Clear()
    {
        _instance = null;
        Destroy(gameObject);
    }

    public void TogglePause()
    {
        IsPause = !IsPause;
        Time.timeScale = (IsPause) ? 0f : 1f;
        UI_InGameEventBus.OnPauseAction?.Invoke();
    }
}