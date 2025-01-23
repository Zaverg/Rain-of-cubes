using System;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    private T _prefab;

    protected T Spawn()
    {
        T obj = Instantiate(_prefab, transform.position, Quaternion.identity);
        
        return obj;
    }

    protected void SetPrefab(T prefab)
    {
        _prefab = prefab;
    }
}
