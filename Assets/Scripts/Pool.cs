using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    private ObjectPool<Cube> _pool;

    public event Func<Cube> Spawned;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(Create, OnGetObject, OnReleaseObject);
    }

    public Cube GetObject()
    {
        return _pool.Get();
    }

    public void OnRelease(Cube obj)
    {
        _pool.Release(obj);
    }

    private Cube Create()
    {
        return Spawned?.Invoke() ?? null;
    }

    private void OnGetObject(Cube obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseObject(Cube obj)
    {
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
    }
}
