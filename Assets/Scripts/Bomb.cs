using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour, IReleased<Bomb>
{
    public event Action<Bomb> Released;

    [SerializeField] private float _second;
    [SerializeField] private float _currentSecond;
    [SerializeField] private float _minSecond;
    [SerializeField] private float _maxSecond;

    [SerializeField] private float _radius;
    [SerializeField] private float _forceExplosion;

    private Material _material;
    [SerializeField] private Color _color;
    [SerializeField] private float _alpha;

    private Color _default;

    private bool _isExplosion;
    private Collider[] _buffer = new Collider[20];

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _default = _material.color;

        _color = new Color(_default.r, _default.g, _default.b, _default.a);
        _material.color = _color;

        _alpha = _color.a;
    }

    private void OnEnable()
    {
        _second = UnityEngine.Random.Range(_maxSecond, _minSecond);
        _currentSecond = _second;
    }

    private void OnDisable()
    {
        _color = _default;
    }

    private void Update()
    {
        if (_currentSecond > 0)
            Disappear();
        else
            Explode();
    }

    private void Disappear()
    {
        _currentSecond -= Time.deltaTime;

        float normalizedTime = _currentSecond / _second;
        _color.a = Mathf.Clamp01(normalizedTime);

        _material.color = _color;
    }

    private void Explode()
    {
        int countHits = Physics.OverlapSphereNonAlloc(transform.position, _radius, _buffer);

        foreach (Collider collider in _buffer)
        {
            if (collider != null && collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_forceExplosion, transform.position, _radius);
        }

        Released?.Invoke(this);
    }
}
