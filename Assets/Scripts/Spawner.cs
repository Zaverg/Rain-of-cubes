using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _maxPositionX;
    [SerializeField] private int _minPositionX;

    [SerializeField] private Cube _prefab;
    [SerializeField] private Pool _pool;

    [SerializeField] private int _time = 0;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_time);
    }

    private void OnEnable()
    {
        _pool.Spawned += Spawn;
        _coroutine = StartCoroutine(Working());
    }

    private void OnDisable()
    {
        _pool.Spawned -= Spawn;
        StopCoroutine(_coroutine);
    }

    private void SetPosition(Cube cube)
    {
        int positionX = UnityEngine.Random.Range(_minPositionX, _maxPositionX);
        cube.transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
    }

    private Cube Spawn()
    {
        Cube obj = Instantiate(_prefab, transform.position, Quaternion.identity);

        return obj;
    }

    private void OnRelease(Cube cube)
    {
        cube.MeshRenderer.material.color = _prefab.MeshRenderer.sharedMaterial.color;
        cube.transform.rotation = _prefab.transform.rotation;
        cube.Rigidbody.isKinematic = true;
        _pool.OnRelease(cube);
        cube.Released -= OnRelease;
    }

    private IEnumerator Working()
    {
        while (enabled)
        {
            yield return _wait;

            Cube cube = _pool.GetObject();
            cube.Rigidbody.isKinematic = false;
            cube.Released += OnRelease;
            SetPosition(cube);
        }
    }
}
