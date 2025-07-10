using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private SpawnerInfo _info;
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;
    private HashSet<T> _objects;

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
        _info.AddSpawned();

        return _pool.Get();
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
        _info.AddCreated();
        
        return obj;
    }

    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
        _info.ChangeActive(_pool.CountActive);
    }

    private void OnReleaseObject(T obj)
    {
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
        _info.ChangeActive(_pool.CountActive);
    }
}
