using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D _rb;
    float _dashSpeed = 12.5f;

    public bool IsDash { get; private set; }
    private float _dashCoolTime = 0.5f;

    Silhouette _silhouette;
    Coroutine _dashCoroutine;
    // ParticleSystem _particlesysetem;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _silhouette = GetComponent<Silhouette>();
        // _particlesysetem = Camera.main.GetComponentInChildren<ParticleSystem>();
        Managers.Input.dashAction += Dash;
    }

    public void Dash(Vector2 direction)
    {
        if (_dashCoroutine == null)
            _dashCoroutine = StartCoroutine(DashCoroutine(direction));
    }

    IEnumerator DashCoroutine(Vector2 direction)
    {
        DrawArea();

        IsDash = true;
        _silhouette.IsActive = true;                        // 대시 중 실루엣 활성화
        _rb.linearVelocity = direction * _dashSpeed;
        // if (_particlesysetem) _particlesysetem.Play();
        yield return new WaitForSeconds(_dashCoolTime);     // 대시 쿨타임
        IsDash = false;
        _silhouette.IsActive = false;                       // 대시 중 실루엣 비활성화
        _dashCoroutine = null;
    }

    int startIdx = 0;
    int maxIdx = 4;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] PolygonCollider2D _polygonCollider2D;
    [SerializeField] List<Vector2> _dashStartList = new List<Vector2>(); // 대시 시작 위치 리스트
    public void DrawArea()
    {
        _lineRenderer.SetPosition(startIdx, transform.position);
        _dashStartList.Add(transform.position);
        startIdx++;

        if (startIdx >= maxIdx)
        {
            _lineRenderer.SetPosition(startIdx, _lineRenderer.GetPosition(0));
            _polygonCollider2D.SetPath(0, _dashStartList.ToArray());
            _lineRenderer.enabled = true;
            _polygonCollider2D.enabled = true;
        }
    }
}