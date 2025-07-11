using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour where T : Component, IReleasable<T>
{   
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private List<TextMeshProUGUI> textMeshProUGUIs = new List<TextMeshProUGUI>();

    private List<string> _startStrs = new List<string>();

    protected virtual void Awake()
    {
        foreach (TextMeshProUGUI str in textMeshProUGUIs)
        {
            _startStrs.Add(str.text);
        }

        _spawner.Spawned += UpdateViewSpawned;
        _spawner.Created += UpdateViewCreated;
        _spawner.Activated += UpdateViewActivated;
    }

    private void UpdateViewSpawned(int count)
    {
        int index = 0;

        textMeshProUGUIs[index].text = _startStrs[index] + count;
    }

    private void UpdateViewCreated(int count)
    {
        int index = 1;

        textMeshProUGUIs[index].text = _startStrs[index] + count;
    }

    private void UpdateViewActivated(int count)
    {
        int index = 2;

        textMeshProUGUIs[index].text = _startStrs[index] + count;
    }
}
