using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    private ObjectPool<T> _pool;
    private HashSet<T> _objects;

    protected virtual void Awake()
    {
        if (_prefab == null)
        {
            Debug.LogError($"Prefab for {typeof(T).Name} is not assigned!", this);
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
            IReleased<T> released = obj as IReleased<T>;
            released.Released -= _pool.Release;
        }
    }

    protected T GetObject()
    {
        return _pool.Get();
    }

    private T Create()
    {
        T obj = Instantiate(_prefab, transform.position, Quaternion.identity);

        if (obj is IReleased<T>)
        {
            IReleased<T> released = obj as IReleased<T>;
            released.Released += _pool.Release;
        }
      
        _objects.Add(obj);
        
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
    }
}
