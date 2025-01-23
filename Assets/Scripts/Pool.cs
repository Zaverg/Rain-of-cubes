using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    private ObjectPool<T> _pool;

    public event Func<T> Spawned;

    [field: SerializeField] public int Size { get; private set; }

    private void Awake()
    {
        _pool = new ObjectPool<T>(Create, OnGetObject, OnReleaseObject, defaultCapacity: 10, maxSize: Size);
    }

    private T Create()
    {
        return Spawned?.Invoke() ?? null;
    }

    private void OnGetObject(T obj)
    {
        obj.transform.gameObject.SetActive(true);
    }

    private void OnReleaseObject(T obj)
    {
        obj.transform.position = transform.position;
        obj.transform.gameObject.SetActive(false);
    }

    public virtual T GetObject()
    {
        return _pool.Get();
    }

    public virtual void OnRelease(T obj)
    {
        _pool.Release(obj);
    }
}
