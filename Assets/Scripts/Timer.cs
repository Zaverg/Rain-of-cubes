using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeMin;
    [SerializeField] private float _timeMax;

    private float _seconds;
    private float _currentSecond;

    public event Action Ended;

    public float Seconds => _seconds;
    public float CurrentSecond => _currentSecond;

    public void StartRun()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        _seconds = UnityEngine.Random.Range(_timeMin, _timeMax);
        _currentSecond = _seconds;

        while (enabled)
        {
            _currentSecond -= Time.deltaTime;

            if (_currentSecond <= 0)
                Ended?.Invoke();

            yield return null;
        }
    }
}
