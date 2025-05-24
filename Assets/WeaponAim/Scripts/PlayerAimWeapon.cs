using System;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour 
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs 
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    Transform _aimTransform;
    Transform _aimGunEndPointTransform;
    Transform _shellPositionTransform;
    Animator _anim;

    [SerializeField] GameObject _ammoPrefab;

    float _shootPower = 50f;

    Vector2 _prevMouseWorldPos;

    void Awake() 
    {
        _aimTransform = transform.Find("Aim");
        _anim = _aimTransform.GetComponent<Animator>();
        _aimGunEndPointTransform = _aimTransform.Find("GunEndPointPosition");
        _shellPositionTransform = _aimTransform.Find("ShellPosition");
    }

    void Update() 
    {
        Reload();
        Aim();
        Shoot();
    }

    void Aim()
    {
        if (_prevMouseWorldPos != Managers.Input.MouseWorldPos)
        {
            _prevMouseWorldPos = Managers.Input.MouseWorldPos;

            // 마우스 월드 좌표를 이용해 Aim 방향을 바라보게 회전
            Vector3 mousePosition = Managers.Input.MouseWorldPos;
            Vector3 aimDirection = (mousePosition - _aimTransform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _aimTransform.eulerAngles = new Vector3(0, 0, angle);

            // 각도에 따라 Flip
            Vector3 aimLocalScale = Vector3.one;
            aimLocalScale.y = (angle > 90 || angle < -90) ? -1f : +1f;
            _aimTransform.localScale = aimLocalScale;
        }
    }

    void Shoot() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Managers.Input.MouseWorldPos;

            _anim.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs 
            {
                gunEndPointPosition = _aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                shellPosition = _shellPositionTransform.position,
            });

            CameraController.Instance.ShakeCamera(0.75f, 0.1f);  // 카메라 흔들기

            GameObject ammoInstance = Instantiate(_ammoPrefab, _aimGunEndPointTransform.position, Quaternion.identity);
            ammoInstance.transform.right = _aimGunEndPointTransform.right;
            ammoInstance.GetComponent<Rigidbody2D>().AddForce(ammoInstance.transform.right * _shootPower, ForceMode2D.Impulse);
        }
    }

    void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("재장전");
        }
    }
}