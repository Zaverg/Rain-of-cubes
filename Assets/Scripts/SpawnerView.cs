using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private SpawnerInfo _info;
    [SerializeField] private List<TextMeshProUGUI> textMeshProUGUIs = new List<TextMeshProUGUI>();

    [SerializeField] private List<string> _startStrings = new List<string>();

    private void Awake()
    {
        _info.Spawned += UpdateView;
        _info.Created += UpdateView;
        _info.Activated += UpdateView;
    }

    private void UpdateView(int index, int info)
    {
        TextMeshProUGUI textMeshProUGUI = textMeshProUGUIs[index];

        if (textMeshProUGUI != null)
            textMeshProUGUI.text = _startStrings[index] + info;
    }
}
