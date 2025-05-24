using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject _ammoPrefab;

    int _totalAmmoCount = 90;         // 전체 총알 수
    int _reloadedAmmoCount = 30;      // 장전된 총알 수
    int _magazineCapacity = 30;       // 탄창 용량
    float _shootDelay = 0.1f;         // 발사 딜레이

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
    }

    void Reload()
    {

    }
}
