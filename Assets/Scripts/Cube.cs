using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger), (typeof(Timer)))]
public class Cube : MonoBehaviour, IReleasable<Cube>, IPositionProvider
{
    private Rigidbody _rigidbody;
    private Timer _timer;
    private ColorChanger _colorChanging;

    private bool IsCollision;

    public event Action<Cube> Released;

    private Vector3 _lastPosition;

    public Vector3 LastPosition => _lastPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _timer = GetComponent<Timer>();
        _colorChanging = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _timer.TimerEnded += Reset;
    }

    private void OnDisable()
    {
        _timer.TimerEnded -= Reset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Platform platform))
        {
            if (IsCollision == false)
            {
                IsCollision = true;

                _colorChanging.Change();
                _timer.StartTimer();
            }
        }
    }

    private void Reset()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        IsCollision = false;

        _lastPosition = transform.position;
        Released?.Invoke(this);
    }
}
