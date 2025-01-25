using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
    }

    private void OnDisable()
    {
        _meshRenderer.material.color = _defaultColor;
    }

    public void Change()
    {
        _meshRenderer.material.color = UnityEngine.Random.ColorHSV();
    }
}
