using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Exploder), typeof(Disappearance), typeof(Timer))]
public class Bomb : MonoBehaviour, IReleasable<Bomb>
{
    private Exploder _exploder;
    private Disappearance _disappearance;
    private Timer _timer;

    public event Action<Bomb> Released;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _disappearance = GetComponent<Disappearance>();
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _disappearance.Reset();
        _timer.Ended += Reset;

        StartCoroutine(Run());
    }

    private void OnDisable()
    {
        _timer.Ended -= Reset;
    }

    private IEnumerator Run()
    {
        _timer.StartRun();

        while (enabled)
        {
            _disappearance.Disappear(Mathf.Clamp01(_timer.CurrentSecond / _timer.Seconds));

            yield return null;
        }
    }

    private void Reset()
    {
        _exploder.Explode();
        Released?.Invoke(this);
    }
}
