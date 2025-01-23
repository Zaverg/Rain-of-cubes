using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private PoolCubs _poolCubs;

    public MeshRenderer MeshRenderer;
    public Rigidbody Rigidbody;

    public bool IsStart { get; private set; }

    private void Awake()
    {
        _poolCubs = FindFirstObjectByType<PoolCubs>();
        MeshRenderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        IsStart = false;
    }

    public void StartTimer(float time)
    {
        IsStart = true;
        StartCoroutine(nameof(Timer), time);
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time); ;
        _poolCubs.OnRelease(this);
    }
}
