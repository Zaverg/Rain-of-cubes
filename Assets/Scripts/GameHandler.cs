using System.Collections;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private int _time = 0;
    [SerializeField] private SpawnerCubes _spawner;
    [SerializeField] private PoolCubs _pool;

    private WaitForSeconds _wait;
 
    private void Awake()
    {
        _wait = new WaitForSeconds(_time);
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(GameWork));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(GameWork));
    }

    private IEnumerator GameWork()
    {
        while (true)
        {
            yield return _wait;

            Cube obj = _pool.GetObject();
            _spawner.SetPosition(obj);
        }
    }
}
