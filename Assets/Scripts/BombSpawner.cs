using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Spawn(Cube cube)
    {
        Bomb bomb = GetObject();
        bomb.transform.position = cube.LastPosition;

        cube.Released -= Spawn;
    }
}
