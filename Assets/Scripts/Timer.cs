using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeLivesOfCubeMin;
    [SerializeField] private float _timeLivesOfCubeMax;

    public event Action TimerEnded;

    public void StartTimer()
    {
        float time = UnityEngine.Random.Range(_timeLivesOfCubeMin, _timeLivesOfCubeMax);
        StartCoroutine(ExecuteTimer(time));
    }

    private IEnumerator ExecuteTimer(float time)
    {
        yield return new WaitForSeconds(time);
        TimerEnded?.Invoke();
    }
}
