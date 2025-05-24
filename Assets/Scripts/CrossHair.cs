using UnityEngine;

public class CrossHair : MonoBehaviour
{
    Vector2 _prevMouseWorldPos;

    void Start()
    {
        Cursor.visible = false; // 커서 숨기기
    }

    void Update()
    {
        if(_prevMouseWorldPos != Managers.Input.MouseWorldPos)
        {
            _prevMouseWorldPos = Managers.Input.MouseWorldPos;
            transform.position = Managers.Input.MouseWorldPos;
        }
    }
}