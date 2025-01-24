using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    private ObjectPool<Cube> _pool;

    public event Func<Cube> Spawned;
    
    [field: SerializeField] public int Size { get; private set; }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(Create, OnGetObject, OnReleaseObject, defaultCapacity: 10, maxSize: Size);
    }

    private Cube Create()
    {
        return Spawned?.Invoke() ?? null;
    }

    private void OnGetObject(Cube obj)
    {
        obj.transform.gameObject.SetActive(true);
    }

    private void OnReleaseObject(Cube obj)
    {
        obj.transform.position = transform.position;
        obj.transform.gameObject.SetActive(false);
    }

    public Cube GetObject()
    {
        return _pool.Get();
    }

    public void OnRelease(Cube obj)
    {
        _pool.Release(obj);
    }
}
