using System;
using UnityEngine;

[RequireComponent(typeof(Exploder), typeof(Disappearance))]
public class Bomb : MonoBehaviour, IReleasable<Bomb>
{
    public event Action<Bomb> Released;

    [SerializeField] private float _minSecond;
    [SerializeField] private float _maxSecond;

    [SerializeField] private Exploder _exploder;
    [SerializeField] private Disappearance _disappearance;

    private float _second;
    [SerializeField] private float _currentSecond;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _disappearance = GetComponent<Disappearance>();
    }

    private void OnEnable()
    {
        _disappearance.Reset();
        _second = UnityEngine.Random.Range(_maxSecond, _minSecond);
        _currentSecond = _second;
    }

    private void Update()
    {
        if (_currentSecond > 0)
        {
            float normalizedTime = Mathf.Clamp01(_currentSecond / _second);
            _disappearance.Disappear(normalizedTime);
            _currentSecond -= Time.deltaTime;
        }
        else
        {
            _exploder.Explode();
            Released?.Invoke(this);
        }
    }
}
