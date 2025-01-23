using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _time;
    private WaitForSeconds _wait;
    private PoolCubs _poolCubs;

    public MeshRenderer MeshRenderer;
    public Rigidbody Rigidbody;

    public bool IsStart { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_time);
        _poolCubs = FindFirstObjectByType<PoolCubs>();
        MeshRenderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        IsStart = false;
    }

    public void StartTimer()
    {
        IsStart = true;
        StartCoroutine(nameof(Timer));
    }

    private IEnumerator Timer()
    {
        yield return _wait;
        _poolCubs.OnRelease(this);
    }
}
