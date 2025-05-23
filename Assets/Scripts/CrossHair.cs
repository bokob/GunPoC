using UnityEngine;

public class CrossHair : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false; // 커서 숨기기
    }

    void Update()
    {
        transform.position = Managers.Input.MouseWorldPos;
    }
}