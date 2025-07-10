using UnityEngine;

public class Disappearance : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private float _alpha;

    private Material _material;
    private Color _default;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _default = _material.color;

        _color = new Color(_default.r, _default.g, _default.b, _default.a);
        _material.color = _color;

        _alpha = 1;
    }

    public void Disappear(float normalizedTime)
    {
        float inversion = 1 - normalizedTime;

        _color.a = Mathf.Lerp(_alpha, 0, inversion);

        _material.color = _color;
    }

    public void Reset()
    {
        _color = _default;
    }
}
