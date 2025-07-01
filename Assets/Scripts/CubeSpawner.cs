using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube> 
{
    [SerializeField] private int _maxPositionX;
    [SerializeField] private int _minPositionX;

    [SerializeField] private int _time = 0;
    [SerializeField] private BombSpawner _bombSpawner;

    private WaitForSeconds _wait;

    private Coroutine _coroutine;

    protected override void Awake()
    {
        base.Awake();
        _wait = new WaitForSeconds(_time);
        _coroutine = StartCoroutine(Working());
    }

    private IEnumerator Working()
    {
        while (enabled)
        {
            yield return _wait;

            Cube cube = GetObject();
            cube.Released += _bombSpawner.Spawn;

            int positionX = Random.Range(_minPositionX, _maxPositionX);
            cube.transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        }
    }
}

