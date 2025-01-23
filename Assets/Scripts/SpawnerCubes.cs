using UnityEngine;

public class SpawnerCubes : Spawner<Cube>
{
    [SerializeField] private int _maxPositionX;
    [SerializeField] private int _minPositionX;

    [SerializeField] private Cube _prefab;
    [SerializeField] private PoolCubs _pool;

    private void Awake()
    {
        SetPrefab(_prefab);
    }

    private void OnEnable()
    {
        _pool.Spawned += Spawn;
    }

    private void OnDisable()
    {
        _pool.Spawned -= Spawn;
    }

    public void SetPosition(Cube cube)
    {
        int positionX = UnityEngine.Random.Range(_minPositionX, _maxPositionX);
        cube.transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
    }
}
