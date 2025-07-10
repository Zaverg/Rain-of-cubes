using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void Spawn<T>(T obj) where T : Component, IPositionProvider, IReleasable<T>
    {
        Bomb bomb = GetObject();
        bomb.transform.position = obj.LastPosition;

        obj.Released -= Spawn;
    }
}
