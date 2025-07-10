using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    public event Action<int, int> Spawned;
    public event Action<int, int> Created;
    public event Action<int, int> Activated;

    private List<int> _counts = new List<int>() { 0, 0, 0};

    public void AddSpawned()
    {
        int index = 0;

        _counts[index]++;
        Spawned?.Invoke(index, _counts[index]);
    }

    public void AddCreated()
    {
        int index = 1;

        _counts[index]++;
        Created?.Invoke(index, _counts[index]);
    }

    public void ChangeActive(int count)
    {
        if (count <= 0)
            return;

        int index = 2;

        _counts[index] = count;
        Activated?.Invoke(index, _counts[index]);
    }
}
