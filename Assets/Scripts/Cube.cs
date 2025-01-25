using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody), typeof(ColorChanger))]
[RequireComponent(typeof(Timer))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Timer _timer;
    private ColorChanger _colorChanging;

    private bool IsCollision;

    public event Action<Cube> Released;

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
        if (collision.transform.TryGetComponent<Platform>(out Platform platform))
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
        transform.rotation = Quaternion.EulerRotation(Vector3.zero);
        IsCollision = false;
        Released?.Invoke(this);
    }
}
