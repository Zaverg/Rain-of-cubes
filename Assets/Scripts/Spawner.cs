using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;
    private HashSet<T> _objects;

    private int _count;

    public event Action<int> Spawned;
    public event Action<int> Created;
    public event Action<int> Activated;

    protected virtual void Awake()
    {
        if (_prefab == null)
        {
            enabled = false;

            return;
        }

        _pool = new ObjectPool<T>(Create, OnGetObject, OnReleaseObject);
        _objects = new HashSet<T>();
    }

    private void OnDisable()
    {
        foreach (T obj in _objects)
        {
            IReleasable<T> released = obj as IReleasable<T>;
            released.Released -= _pool.Release;
        }
    }

    protected T GetObject()
    {
        T obj = _pool.Get();

        _count++;
        Spawned?.Invoke(_count);
        Activated?.Invoke(_pool.CountActive);

        return obj;
    }
     
    private T Create()
    {
        T obj = Instantiate(_prefab, transform.position, Quaternion.identity);

        if (obj is IReleasable<T>)
        {
            IReleasable<T> released = obj as IReleasable<T>;
            released.Released += _pool.Release;
        }

        _objects.Add(obj);

        Created?.Invoke(_objects.Count);

        return obj;
    }

    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseObject(T obj)
    {
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);

        Activated?.Invoke(_pool.CountActive - 1);
    }
}
